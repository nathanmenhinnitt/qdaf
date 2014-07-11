namespace qdaf.Framework
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class Transaction
    {
        private bool _transactionStarted;
        private readonly int _timeout;
        private readonly ITimer _timer;
        private readonly ITransactionProcessor _transactionProcessor;
        public bool TransactionStarted { get; set; }
        public Guid TransactionId { get; set; }
        private readonly Lazy<List<TransactionLock>> _locks;
        public List<TransactionLock> Locks
        {
            get
            {
                return _locks.Value;
            }
        }

        public Transaction()
            : this(Defaults.Timeout, new DefaultTimer(), new DefaultTransactionProcessor())
        {

        }

        public Transaction(int timeout, ITimer timer, ITransactionProcessor transactionProcessor)
        {
            TransactionId = Guid.NewGuid();
            _transactionStarted = false;
            _timeout = timeout;
            _timer = timer;
            _transactionProcessor = transactionProcessor;
            _locks = new Lazy<List<TransactionLock>>(() => new List<TransactionLock>());
            _timer = new DefaultTimer { Timeout = _timeout };
        }

        public async Task<TransactionBatchResult> BeginTransactionAsync(DataPacket packet)
        {
            if (_transactionStarted)
            {
                return new TransactionBatchResult
                    {
                        Status = TransactionStaus.NotStarted,
                        TransactionId = TransactionId,
                        Exceptions = new []{new Exception("BeginTransaction has already been called"), }
                    };
            }

            _transactionStarted = true;

            Exception thrown = null;
            TransactionStaus thrownStatus = 0;

            try
            {
                _timer.Start();
                _timer.TimeoutOccurred += delegate(object sender, TransactionException timeoutException)
                    {
                        thrown = timeoutException;
                        thrownStatus = timeoutException.ThrownStatus;
                    };

                _transactionProcessor.ProcessTransaction(this, packet);

                if (thrown != null)
                {
                    await RollbackTransactionAsync();

                    return new TransactionBatchResult
                        {
                            Status = Enum.IsDefined(typeof(TransactionStaus), thrownStatus) ? thrownStatus : TransactionStaus.Failed,
                            TransactionId = TransactionId
                        };
                }

                return await Task.FromResult(new TransactionBatchResult
                    {
                        Status = TransactionStaus.Success,
                        TransactionId = TransactionId
                    });
            }
            catch (TransactionException ex)
            {
                thrownStatus = ex.ThrownStatus;
                thrown = ex;
            }
            catch (Exception ex)
            {
                _timer.Stop();
                thrown = ex;
            }

            return await Task.FromResult(new TransactionBatchResult(new[] { thrown })
                {
                    Status = Enum.IsDefined(typeof(TransactionStaus), thrownStatus) ? thrownStatus : TransactionStaus.Failed,
                    TransactionId = TransactionId
                });
        }

        private void AddTransactionLock(TransactionLock transactionLock)
        {
            transactionLock.TransactionFailed += async delegate
            {
                await TransactionLockOnTransactionFailedAsync();
            };

            transactionLock.TransactionStarted += TransactionLockOnTransactionStarted;

            _locks.Value.Add(transactionLock);
        }

        private void TransactionLockOnTransactionStarted(object sender, EventArgs eventArgs)
        {
            if (TransactionStarted)
            {
                return;
            }

            _timer.TimeoutOccurred += async delegate
                {
                    await RollbackTransactionAsync();
                };

            _timer.Start();
        }

        private async Task TransactionLockOnTransactionFailedAsync()
        {
            await RollbackTransactionAsync();
        }

        private async Task RollbackTransactionAsync()
        {
            foreach (var transactionLock in Locks)
            {
                await transactionLock.RollbackAsync();
            }
        }
    }
}
