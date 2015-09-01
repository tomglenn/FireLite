namespace FireLite.Core.Interfaces
{
    public interface IClient
    {
        string Host { get; set; }
        int Port { get; set; }

        bool Connect();
        void Disconnect();
        void SendPacket(byte[] packetBytes);
    }
}
