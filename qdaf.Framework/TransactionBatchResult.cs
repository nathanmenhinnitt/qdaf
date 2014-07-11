namespace qdaf.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TransactionBatchResult
    {
        public TransactionBatchResult()
            : this(null)
        {

        }

        public TransactionBatchResult(IEnumerable<Exception> transactionExceptions)
        {
            var exceptions = transactionExceptions as Exception[] ?? transactionExceptions.ToArray();

            if (transactionExceptions == null || !exceptions.Any())
            {
                return;
            }

            Exceptions = exceptions;
        }

        public Guid TransactionId { get; set; }

        public TransactionStaus Status { get; set; }

        public IEnumerable<Exception> Exceptions { get; set; }
    }
}
