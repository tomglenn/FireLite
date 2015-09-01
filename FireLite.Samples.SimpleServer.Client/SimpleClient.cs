using System;
using FireLite.Core.Extensions;

namespace FireLite.Samples.SimpleServer.Client
{
    public class SimpleClient : Core.Network.Client
    {
        public SimpleClient(string host, int port) : base(host, port) { }

        protected override void OnPacketReceived(byte[] packetBytes)
        {
            var packetString = packetBytes.GetString();

            Console.WriteLine("Got packet: {0}", packetString);
        }
    }
}
