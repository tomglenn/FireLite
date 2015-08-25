using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FireLite.Core.Extensions;
using FireLite.Core.Interfaces;

namespace FireLite.Core.Network
{
    public abstract class Server : IServer
    {
        public int Port { get; set; }

        protected IList<ClientConnection> ConnectedClients { get; private set; }

        private readonly TcpListener tcpListener;
        private Thread listenThread;

        protected Server(int port)
        {
            Port = port;
            tcpListener = new TcpListener(IPAddress.Any, Port);
            ConnectedClients = new List<ClientConnection>();
        }

        public void Start()
        {
            tcpListener.Start();
            
            listenThread = new Thread(ListenForClients);
            listenThread.Start();

            OnStarted();
        }

        public void Stop()
        {
            tcpListener.Stop();
            listenThread.Abort();

            OnStopped();
        }

        public void NotifyClientDisconnected(ClientConnection clientConnection)
        {
            var connectedClient = ConnectedClients.FirstOrDefault(x => x.Id == clientConnection.Id);
            if (connectedClient != null)
            {
                ConnectedClients.Remove(connectedClient);
            }

            OnClientDisconnected(clientConnection);
        }

        public virtual void OnStarted()
        {
            Console.WriteLine("Server started on port {0}", Port);
        }

        public virtual void OnStopped()
        {
            Console.WriteLine("Server stopped");
        }

        public virtual void OnClientConnected(ClientConnection clientConnection)
        {
            Console.WriteLine("Client connected: {0}", clientConnection.Id);
        }

        public virtual void OnClientDisconnected(ClientConnection clientConnection)
        {
            Console.WriteLine("Client disconnected: {0}", clientConnection.Id);
        }

        public virtual void OnClientPacketReceived(ClientConnection clientConnection, byte[] packetBytes)
        {
            Console.WriteLine("Packet received from client {0}:\n{1}", clientConnection.Id, packetBytes.GetString());
        }

        private void ListenForClients()
        {
            while (true)
            {
                try
                {
                    var client = tcpListener.AcceptTcpClient();
                    var clientThread = new Thread(HandleClientConnection);
                    clientThread.Start(client);
                }
                catch (ObjectDisposedException)
                {
                    listenThread.Abort();
                }
            }
        }

        private void HandleClientConnection(object client)
        {
            var clientConnection = new ClientConnection(this, (TcpClient) client);
            ConnectedClients.Add(clientConnection);

            OnClientConnected(clientConnection);
        }
    }
}
