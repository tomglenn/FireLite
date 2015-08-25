using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FireLite.Core.Exceptions;
using FireLite.Core.Extensions;
using FireLite.Server.Interfaces;

namespace FireLite.Server
{
    public abstract class Server : IServer
    {
        public int Port { get; set; }

        private readonly TcpListener tcpListener;
        private Thread listenThread;

        protected Server(int port)
        {
            Port = port;
            tcpListener = new TcpListener(IPAddress.Any, Port);
        }

        public virtual void Start()
        {
            tcpListener.Start();
            
            listenThread = new Thread(ListenForClients);
            listenThread.Start();

            Console.WriteLine("Server started on port {0}", Port);
        }

        public virtual void Stop()
        {
            listenThread.Abort();
            tcpListener.Stop();
            Console.WriteLine("Server stopped");
        }

        private void ListenForClients()
        {
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                var clientThread = new Thread(HandleClientConnection);
                clientThread.Start(client);
            }
        }

        private void HandleClientConnection(object client)
        {
            var tcpClient = (TcpClient) client;
            var clientStream = tcpClient.GetStream();

            clientStream.SendPacket("Hello Mr Client".GetBytes());
            
            Console.WriteLine("Client connected: {0}", tcpClient.Client.RemoteEndPoint);

            var connected = true;
            while (connected)
            {
                try
                {
                    clientStream.ReadPacket();
                }
                catch (ConnectionException ex)
                {
                    tcpClient.Close();
                    connected = false;
                    Console.WriteLine("Client disconnected");
                }
            }
        }
    }
}
