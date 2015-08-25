using System;

namespace FireLite.Samples.SimpleServer.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SimpleClient("127.0.0.1", 1337);
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
    }
}
