using System;

namespace FireLite.Core.Interfaces
{
    public interface IClientConnection
    {
        Guid Id { get; }

        void Disconnect();
        void SendPacket(byte[] packetBytes);
    }
}
