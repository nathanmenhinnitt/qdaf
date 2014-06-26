namespace qdaf.Core
{
    using System.Threading.Tasks;

    public interface IPacketProcessor
    {
        Task ProcessAsync(DataPacket dataPacket);
    }
}
