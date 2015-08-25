using System;
using System.Net.Sockets;
using FireLite.Core.Exceptions;

namespace FireLite.Core.Extensions
{
    public static class NetworkStreamExtensions
    {
        public static byte[] ReadNBytes(this NetworkStream networkStream, int n)
        {
            var buffer = new byte[n];
            var bytesRead = 0;

            while (bytesRead < n)
            {
                var chunk = networkStream.Read(buffer, bytesRead, buffer.Length - bytesRead);
                if (chunk == 0)
                {
                    throw new ConnectionException();
                }

                bytesRead += chunk;
            }

            return buffer;
        }

        public static byte[] ReadPacket(this NetworkStream networkStream)
        {
            var lengthBuffer = networkStream.ReadNBytes(4);
            var packetLength = BitConverter.ToInt32(lengthBuffer, 0);

            var buffer = networkStream.ReadNBytes(packetLength);
            return buffer;
        }

        public static void SendPacket(this NetworkStream networkStream, byte[] packetBytes)
        {
            var lengthBuffer = BitConverter.GetBytes(packetBytes.Length);
            networkStream.Write(lengthBuffer, 0, lengthBuffer.Length);
            networkStream.Write(packetBytes, 0, packetBytes.Length);
            networkStream.Flush();
        }
    }
}
