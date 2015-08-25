namespace FireLite.Core.Exceptions
{
    public class ConnectionException : FireLiteException
    {
        public ConnectionException() : base("The client disconnected unexpectedly")
        {
            
        }

        public ConnectionException(string message) : base(message)
        {
            
        }
    }
}
