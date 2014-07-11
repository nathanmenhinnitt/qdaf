namespace qdaf.Framework
{
    using System;

    public interface ITimer
    {
        int Timeout { get; set; }
        event EventHandler<TransactionException> TimeoutOccurred;
        void Start();
        void Stop();
    }
}
