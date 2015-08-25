namespace FireLite.Server.Interfaces
{
    public interface IServer
    {
        int Port { get; set; }

        void Start();
        void Stop();
    }
}
