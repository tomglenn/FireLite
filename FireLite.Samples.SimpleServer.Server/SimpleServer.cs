using System.Threading;
using FireLite.Core.Extensions;

namespace FireLite.Samples.SimpleServer.Server
{
    public class SimpleServer : Core.Network.Server
    {
        public SimpleServer(int port) : base(port)
        {
            var timer = new Timer(SendMessage, null, 5000, 5000);
        }

        private void SendMessage(object state)
        {
            foreach (var connectedClient in ConnectedClients)
            {
                connectedClient.SendPacket("Hey man".GetBytes());
            }
        }
    }
}
