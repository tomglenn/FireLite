using System;
using System.Collections.Generic;
using FireLite.Samples.JsonPacketServer.Core.Network.Enums;

namespace FireLite.Samples.JsonPacketServer.Core.Network.Packets
{
    public static class PacketLookup
    {
        public static IDictionary<OpCode, Type> OpCodeToPacketType = new Dictionary<OpCode, Type>()
        {
            {
                OpCode.Hello, typeof (HelloPacket)
            }
        };
    }
}
