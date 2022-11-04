using System;

namespace lkcode.hetznercloudapi.Exceptions
{
    public class InvalidAccessTokenException : Exception
    {
        public InvalidAccessTokenException()
        {
        }

        public InvalidAccessTokenException(string message)
            : base(message)
        {
        }

        public InvalidAccessTokenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}