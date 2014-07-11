namespace qdaf.Framework
{
    public enum TransactionStaus : byte
    {
        NotStarted = 1,
        InProgress = 2,
        Success = 3,
        Failed = 4,
        Deadlock = 5,
        Timeout = 6
    }
}
