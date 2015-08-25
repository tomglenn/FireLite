using System;

namespace FireLite.Samples.SimpleServer.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new SimpleServer(1337);
            server.Start();
            Console.ReadKey();
            server.Stop();
        }
    }
}
