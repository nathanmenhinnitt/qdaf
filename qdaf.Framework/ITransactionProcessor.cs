namespace qdaf.Framework
{
    using Core;

    public interface ITransactionProcessor
    {
        void ProcessTransaction(Transaction transaction, DataPacket packet);
    }
}
