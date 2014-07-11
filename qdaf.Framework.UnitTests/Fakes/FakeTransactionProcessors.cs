namespace qdaf.Framework.UnitTests.Fakes
{
    using System.Threading;
    using Core;

    public class LongDelayFakeTransactionProcessors : ITransactionProcessor
    {
        private readonly int _timeout;

        public LongDelayFakeTransactionProcessors()
            : this(250)
        {

        }

        public LongDelayFakeTransactionProcessors(int timeout)
        {
            _timeout = timeout;
        }

        public void ProcessTransaction(Transaction transaction, DataPacket packet)
        {
            Thread.Sleep(_timeout);
        }
    }
}
