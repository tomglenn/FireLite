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

        protected virtual void OnStarted()
        {
            Console.WriteLine("Server started on port {0}", Port);
        }

        protected virtual void OnStopped()
        {
            Console.WriteLine("Server stopped");
        }

        protected virtual void OnClientConnected(ClientConnection clientConnection)
        {
            Console.WriteLine("Client connected: {0}", clientConnection.Id);
        }

        protected virtual void OnClientDisconnected(ClientConnection clientConnection)
        {
            Console.WriteLine("Client disconnected: {0}", clientConnection.Id);
        }

        protected virtual void OnClientPacketReceived(ClientConnection clientConnection, byte[] packetBytes)
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
                    var clientThread = new Thread(HandleClientConnected);
                    clientThread.Start(client);
                }
                catch (ObjectDisposedException)
                {
                    listenThread.Abort();
                }
            }
        }

        private void HandleClientConnected(object client)
        {
            var clientConnection = new ClientConnection((TcpClient) client);
            ConnectedClients.Add(clientConnection);

            clientConnection.Disconnected += HandleClientDisconnected;
            clientConnection.PacketReceived += HandleClientPacketReceived;

            OnClientConnected(clientConnection);
        }

        private void HandleClientDisconnected(ClientConnection clientConnection)
        {
            var connectedClient = ConnectedClients.FirstOrDefault(x => x.Id == clientConnection.Id);
            if (connectedClient != null)
            {
                ConnectedClients.Remove(connectedClient);
            }

            OnClientDisconnected(clientConnection);
        }

        private void HandleClientPacketReceived(ClientConnection clientConnection, byte[] packetBytes)
        {
            OnClientPacketReceived(clientConnection, packetBytes);
        }
    }
}
