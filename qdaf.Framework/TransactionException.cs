namespace qdaf.Framework
{
    using System;

    public class TransactionException : Exception
    {
        public TransactionStaus ThrownStatus { get; set; }

        public TransactionException()
        {

        }

        public TransactionException(TransactionStaus thrownStatus)
        {
            ThrownStatus = thrownStatus;
        }

        public TransactionException(string message)
            : base(message)
        {

        }

        public TransactionException(string message, TransactionStaus thrownStatus)
            : base(message)
        {
            ThrownStatus = thrownStatus;
        }

        public TransactionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public TransactionException(string message, Exception innerException, TransactionStaus thrownStatus)
            : base(message, innerException)
        {
            ThrownStatus = thrownStatus;
        }
    }
}
