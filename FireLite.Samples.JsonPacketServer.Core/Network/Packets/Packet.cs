using FireLite.Samples.JsonPacketServer.Core.Network.Enums;
using FireLite.Samples.JsonPacketServer.Core.Network.Interfaces;

namespace FireLite.Samples.JsonPacketServer.Core.Network.Packets
{
    public class Packet : IPacket
    {
        public OpCode OpCode { get; set; }
    }
}
