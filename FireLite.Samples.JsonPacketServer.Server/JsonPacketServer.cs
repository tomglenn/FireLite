using System;
using System.Threading;
using FireLite.Core.Extensions;
using FireLite.Core.Network;
using FireLite.Samples.JsonPacketServer.Core.Extensions;
using FireLite.Samples.JsonPacketServer.Core.Network.Packets;
using Newtonsoft.Json;

namespace FireLite.Samples.JsonPacketServer.Server
{
    public class JsonPacketServer : FireLite.Core.Network.Server
    {
        public JsonPacketServer(int port) : base(port)
        {
            var timer = new Timer(SendHelloPacketToAllClients, null, 1000, 1000);
        }

        private void SendHelloPacketToAllClients(object state)
        {
            foreach (var connectedClient in ConnectedClients)
            {
                var packet = new HelloPacket("Hello world!");
                SendPacketToClient(connectedClient, packet);
                Console.WriteLine("Sent Packet OpCode.{0} to client {1}", packet.OpCode, connectedClient.Id);
            }
        }

        public void SendPacketToClient(ClientConnection clientConnection, Packet packet)
        {
            var jsonString = JsonConvert.SerializeObject(packet);
            clientConnection.SendPacket(jsonString.GetBytes());
        }

        protected override void OnClientPacketReceived(ClientConnection clientConnection, byte[] packetBytes)
        {
            try
            {
                var packet = packetBytes.ToPacket();
                OnClientPacketReceived(clientConnection, packet);
            }
            catch (Exception)
            {
                Console.WriteLine("Received illegal packet from client: {0}\nKilling client connection.", clientConnection.Id);
                clientConnection.Disconnect();
            }
        }

        protected void OnClientPacketReceived(ClientConnection clientConnection, Packet packet)
        {
            var helloPacket = packet as HelloPacket;
            if (helloPacket != null)
            {
                Console.WriteLine("Got a HelloPacket with values - Message: {0}", helloPacket.Message);
            }
        }
    }
}
