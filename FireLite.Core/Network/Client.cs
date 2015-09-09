using System;
using System.Net.Sockets;
using System.Threading;
using FireLite.Core.Exceptions;
using FireLite.Core.Extensions;
using FireLite.Core.Interfaces;

namespace FireLite.Core.Network
{
    public abstract class Client : IClient
    {
        public string Host { get; set; }
        public int Port { get; set; }

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private Thread listenThread;

        protected Client(string host, int port)
        {
            Host = host;
            Port = port;

            tcpClient = new TcpClient();
        }

        public bool Connect()
        {
            try
            {
                tcpClient.Connect(Host, Port);
            }
            catch (SocketException)
            {
                OnConnectionFailed();
                return false;
            }

            listenThread = new Thread(ListenToServer);
            listenThread.Start();

            OnConnected();

            return true;
        }

        public void Disconnect()
        {
            OnDisconnected();

            tcpClient.Close();

            if (listenThread != null)
            {
                listenThread.Abort();
            }
        }

        public void SendPacket(byte[] packetBytes)
        {
            networkStream.SendPacket(packetBytes);
        }

        protected virtual void OnConnected()
        {
            Console.WriteLine("Connected to server {0}:{1}", Host, Port);
        }

        protected virtual void OnConnectionFailed()
        {
            Console.WriteLine("Connection failed to server {0}:{1}, please try again", Host, Port);
        }

        protected virtual void OnDisconnected()
        {
            Console.WriteLine("Disconnected from server");
        }

        protected virtual void OnPacketReceived(byte[] packetBytes)
        {
            var str = packetBytes.GetString();
            Console.WriteLine("Got Message: {0}", str);
        }

        private void ListenToServer()
        {
            networkStream = tcpClient.GetStream();

            while (true)
            {
                try
                {
                    var packetBytes = networkStream.ReadPacket();
                    OnPacketReceived(packetBytes);
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
