using System;

namespace FireLite.Core.Interfaces
{
    public interface IClientConnection
    {
        Guid Id { get; }

        void SendPacket(byte[] packetBytes);
    }
}
