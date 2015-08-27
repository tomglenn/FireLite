using System;
using FireLite.Core.Extensions;
using FireLite.Samples.JsonPacketServer.Core.Exceptions;
using FireLite.Samples.JsonPacketServer.Core.Network.Packets;
using Newtonsoft.Json;

namespace FireLite.Samples.JsonPacketServer.Core.Extensions
{
    public static class ByteArrayExtensions
    {
        public static Packet ToPacket(this byte[] bytes)
        {
            try
            {
                var packetString = bytes.GetString();
                var packet = JsonConvert.DeserializeObject<Packet>(packetString);
                var packetType = PacketLookup.OpCodeToPacketType[packet.OpCode];

                return (Packet) JsonConvert.DeserializeObject(packetString, packetType);
            }
            catch (Exception)
            {
                throw new IllegalPacketException();
            }
        }
    }
}
