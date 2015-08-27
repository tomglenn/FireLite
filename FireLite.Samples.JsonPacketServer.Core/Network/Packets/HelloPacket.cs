using FireLite.Samples.JsonPacketServer.Core.Network.Enums;

namespace FireLite.Samples.JsonPacketServer.Core.Network.Packets
{
    public class HelloPacket : Packet
    {
        public string Message { get; set; }

        public HelloPacket()
        {
            OpCode = OpCode.Hello;
        }

        public HelloPacket(string message) : this()
        {
            Message = message;
        }
    }
}
