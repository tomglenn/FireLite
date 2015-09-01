using System;
using System.Net.Sockets;
using System.Threading;
using FireLite.Core.Exceptions;
using FireLite.Core.Extensions;
using FireLite.Core.Interfaces;

namespace FireLite.Core.Network
{
    public delegate void ClientConnectionEventHandler(ClientConnection sender);
    public delegate void ClientPacketReceivedEventHandler(ClientConnection sender, byte[] bytes);

    public class ClientConnection : IClientConnection
    {
        public event ClientConnectionEventHandler Disconnected;
        public event ClientPacketReceivedEventHandler PacketReceived;

        public Guid Id { get; private set; }

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private readonly Thread listenThread;

        public ClientConnection(TcpClient tcpClient)
        {
            Id = Guid.NewGuid();
            
            this.tcpClient = tcpClient;

            listenThread = new Thread(ListenToClient);
            listenThread.Start();
        }

        public void Disconnect()
        {
            if (Disconnected != null)
            {
                Disconnected(this);
            }

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

                    if (PacketReceived != null)
                    {
                        PacketReceived(this, packetBytes);
                    }
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
