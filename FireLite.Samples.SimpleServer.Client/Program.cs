using System;

namespace FireLite.Samples.SimpleServer.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to connect");
            Console.ReadKey();

            var client = new SimpleClient("127.0.0.1", 1337);
            client.Connect();

            Console.WriteLine("Press any key to disconnect");
            Console.ReadKey();
            client.Disconnect();
        }
    }
}
