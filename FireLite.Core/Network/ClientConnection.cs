using System;
using System.Net.Sockets;
using System.Threading;
using FireLite.Core.Exceptions;
using FireLite.Core.Extensions;
using FireLite.Core.Interfaces;

namespace FireLite.Core.Network
{
    public class ClientConnection : IClientConnection
    {
        public Guid Id { get; private set; }

        private readonly IServer server;
        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private readonly Thread listenThread;

        public ClientConnection(IServer server, TcpClient tcpClient)
        {
            Id = Guid.NewGuid();
            
            this.server = server;
            this.tcpClient = tcpClient;

            listenThread = new Thread(ListenToClient);
            listenThread.Start();
        }

        public void Disconnect()
        {
            server.NotifyClientDisconnected(this);

            tcpClient.Close();
            listenThread.Abort();
        }

        public void SendPacket(byte[] packetBytes)
        {
            networkStream.SendPacket(packetBytes);
        }

        private void ListenToClient()
        {
            networkStream = tcpClient.GetStream();

            while (true)
            {
                try
                {
                    var packetBytes = networkStream.ReadPacket();
                    server.OnClientPacketReceived(this, packetBytes);
                }
                catch (ConnectionException)
                {
                    Disconnect();
                    break;
                }
            }
        }
    }
}
