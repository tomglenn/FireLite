using FireLite.Samples.JsonPacketServer.Core.Network.Enums;

namespace FireLite.Samples.JsonPacketServer.Core.Network.Interfaces
{
    public interface IPacket
    {
        OpCode OpCode { get; }
    }
}
