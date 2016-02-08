using System;
using System.Collections.Generic;
using FireLite.Core.Network;

namespace FireLite.Core.Extensions
{
    public static class SendPacketExtensions
    {
        public static void SendPacket(this ClientConnection clientConnection, string contents)
        {
            var packetBytes = contents.GetBytes();
            clientConnection.SendPacket(packetBytes);
        }

        public static void SendPacket(this IList<ClientConnection> connectedClients, string contents)
        {
            var packetBytes = contents.GetBytes();
            connectedClients.SendPacket(packetBytes);
        }

        public static void SendPacket(this IList<ClientConnection> connectedClients, byte[] packetBytes)
        {
            foreach (var client in connectedClients)
            {
                client.SendPacket(packetBytes);
            }
        }

        public static void SendPacket(this Client client, string contents)
        {
            var packetBytes = contents.GetBytes();
            client.SendPacket(packetBytes);
        }
    }
}
