namespace FireLite.Client.Interfaces
{
    public interface IClient
    {
        string Host { get; set; }
        int Port { get; set; }

        void Connect();
        void Disconnect();
    }
}
