namespace qdaf.Core
{
    public interface IPacketValidator
    {
        QdafValidationResult Validate(DataPacket dataPacket);
    }
}
