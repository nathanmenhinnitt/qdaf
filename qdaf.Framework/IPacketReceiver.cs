namespace qdaf.Framework
{
    using System.Threading.Tasks;
    using Core;

    public interface IPacketReceiver
    {
        Task ProcessPacketAsync(DataPacket data);
    }
}
