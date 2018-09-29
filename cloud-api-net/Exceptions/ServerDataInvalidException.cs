using System;

namespace lkcode.hetznercloudapi.Exceptions
{
    public class ServerDataInvalidException : Exception
    {
        public ServerDataInvalidException()
        {
        }

        public ServerDataInvalidException(string message)
            : base(message)
        {
        }

        public ServerDataInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}