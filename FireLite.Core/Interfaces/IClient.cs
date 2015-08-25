namespace FireLite.Core.Interfaces
{
    public interface IClient
    {
        string Host { get; set; }
        int Port { get; set; }

        bool Connect();
        void Disconnect();
        void SendPacket(byte[] packetBytes);
        void OnConnected();
        void OnConnectionFailed();
        void OnDisconnected();
        void OnPacketReceived(byte[] packetBytes);
    }
}
