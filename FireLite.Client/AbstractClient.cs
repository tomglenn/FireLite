using System;
using System.Net.Sockets;
using FireLite.Client.Interfaces;
using FireLite.Core.Extensions;

namespace FireLite.Client
{
    public abstract class AbstractClient : IClient
    {
        public string Host { get; set; }
        public int Port { get; set; }

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;

        protected AbstractClient(string host, int port)
        {
            Host = host;
            Port = port;

            tcpClient = new TcpClient();
        }

        public void Connect()
        {
            tcpClient.Connect(Host, Port);
            networkStream = tcpClient.GetStream();

            Console.WriteLine("Connected to server {0}:{1}", Host, Port);

            var packet = networkStream.ReadPacket();
            Console.WriteLine("Got message from server: {0}", packet.GetString());
        }

        public void Disconnect()
        {
            tcpClient.Close();
            Console.WriteLine("Disconnected from server");
        }
    }
}
