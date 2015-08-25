using System;

namespace FireLite.Core.Exceptions
{
    public class FireLiteException : Exception
    {
        public override string Message
        {
            get { return message; }
        }

        private readonly string message;

        public FireLiteException()
        {
        }

        public FireLiteException(string message)
        {
            this.message = message;
        }
    }
}
