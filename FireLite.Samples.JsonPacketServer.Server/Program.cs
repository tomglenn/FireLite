using System;

namespace FireLite.Samples.JsonPacketServer.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new JsonPacketServer(1337);
            server.Start();
            Console.ReadKey();
            server.Stop();
        }
    }
}
