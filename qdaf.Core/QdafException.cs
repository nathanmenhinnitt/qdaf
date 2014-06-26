namespace qdaf.Core
{
    using System;

    public class QdafException : Exception
    {
        public QdafException()
        {
            
        }

        public QdafException(string message)
            :base(message)
        {
            
        }

        public QdafException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
