namespace qdaf.Core
{
    using System.Collections.Generic;

    public interface IPacketCommand
    {
        int Order { get; set; }

        string Command { get; set; }

        byte[] PacketData { get; set; }

        bool Success { get; }

        IEnumerable<string> Errors { get; set; }
    }
}
