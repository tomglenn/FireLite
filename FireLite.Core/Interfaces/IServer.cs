using FireLite.Core.Network;

namespace FireLite.Core.Interfaces
{
    public interface IServer
    {
        int Port { get; set; }

        void Start();
        void Stop();
        void NotifyClientDisconnected(ClientConnection clientConnection);
        void OnStarted();
        void OnStopped();
        void OnClientConnected(ClientConnection clientConnection);
        void OnClientDisconnected(ClientConnection clientConnection);
        void OnClientPacketReceived(ClientConnection clientConnection, byte[] packetBytes);
    }
}
