using System;
using System.Collections.Generic;
using FireLite.Core.Extensions;
using FireLite.Samples.JsonPacketServer.Core.Extensions;
using FireLite.Samples.JsonPacketServer.Core.Network.Packets;
using Newtonsoft.Json;
using FireLite.Samples.JsonPacketServer.Core.Network.Enums;

namespace FireLite.Samples.JsonPacketServer.Client
{
    public class Client : FireLite.Core.Network.Client
    {
        private readonly IDictionary<OpCode, IList<Action<Packet>>> packetHandlers = new Dictionary<OpCode, IList<Action<Packet>>>();

        public Client(string host, int port) : base(host, port)
        {
        }

        public void SendPacket(Packet packet)
        {
            var jsonString = JsonConvert.SerializeObject(packet);
            SendPacket(jsonString.GetBytes());
        }

        protected override void OnPacketReceived(byte[] packetBytes)
        {
            try
            {
                var packet = packetBytes.ToPacket();
                OnPacketReceived(packet);
            }
            catch (Exception)
            {
                Console.WriteLine("Received illegal packet from server: {0}");
            }
        }

        protected void OnPacketReceived(Packet packet)
        {
            if (!packetHandlers.ContainsKey(packet.OpCode))
            {
                return;
            }

            foreach (var packetHandler in packetHandlers[packet.OpCode])
            {
                packetHandler(packet);
            }
        }

        public void RegisterPacketHandler(OpCode opCode, Action<Packet> packetHandler)
        {
            if (!packetHandlers.ContainsKey(opCode))
            {
                packetHandlers.Add(opCode, new List<Action<Packet>>());
            }

            packetHandlers[opCode].Add(packetHandler);
        }
    }
}
