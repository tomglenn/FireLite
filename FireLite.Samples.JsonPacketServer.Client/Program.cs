using System;
using FireLite.Samples.JsonPacketServer.Core.Network.Enums;
using FireLite.Samples.JsonPacketServer.Core.Network.Packets;

namespace FireLite.Samples.JsonPacketServer.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("127.0.0.1", 1337);
            client.RegisterPacketHandler(OpCode.Hello, HandleHelloPacket);

            var connected = false;
            while (!connected)
            {
                Console.WriteLine("Press any key to connect");
                Console.ReadKey();

                connected = client.Connect();
            }

            Console.WriteLine("Press any key to disconnect");
            Console.ReadKey();
            client.Disconnect();
        }

        private static void HandleHelloPacket(Packet packet)
        {
            Console.WriteLine("Received 'Hello' packet from server with message:\n{0}", ((HelloPacket)packet).Message);
        }
    }
}
