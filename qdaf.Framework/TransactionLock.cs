namespace qdaf.Framework
{
    using System;
    using System.Threading.Tasks;

    public class TransactionLock
    {
        private bool _hasTransactionStarted;
        internal event EventHandler<Exception> TransactionFailed;
        internal event EventHandler TransactionStarted;

        public TransactionLock()
        {
            _hasTransactionStarted = false;
        }

        public async Task BeginTransactionAsync()
        {
            _hasTransactionStarted = true;

            try
            {
                TransactionStarted(this, new EventArgs());
            }
            catch (Exception ex)
            {
                TransactionFailed(this, ex);
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (!_hasTransactionStarted)
            {
                return;
            }
        }
    }
}
