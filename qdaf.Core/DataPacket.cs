namespace qdaf.Core
{
    using System.Collections.Generic;

    public class DataPacket
    {
        public IEnumerable<IPacketCommand> Commands { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
