using FireLite.Core.Extensions;

namespace FireLite.Samples.SimpleServer.Client
{
    public class SimpleClient : FireLite.Core.Network.Client
    {
        public SimpleClient(string host, int port) : base(host, port) { }

        public override void OnPacketReceived(byte[] packetBytes)
        {
            base.OnPacketReceived(packetBytes);

            var message = packetBytes.GetString();
            if (message == "Hey man")
            {
                SendPacket("Haha hey dude".GetBytes());
            }
        }
    }
}
