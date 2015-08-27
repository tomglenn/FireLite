using FireLite.Core.Exceptions;

namespace FireLite.Samples.JsonPacketServer.Core.Exceptions
{
    public class IllegalPacketException : FireLiteException
    {
        public IllegalPacketException() { }
        public IllegalPacketException(string message) : base(message) { }
    }
}
