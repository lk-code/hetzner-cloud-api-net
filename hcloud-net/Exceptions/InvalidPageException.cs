using System;
using System.Collections.Generic;
using System.Text;

namespace lkcode.hetznercloudapi.Exceptions
{
    public class InvalidPageException : Exception
    {
        public InvalidPageException()
        {
        }

        public InvalidPageException(string message)
            : base(message)
        {
        }

        public InvalidPageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}