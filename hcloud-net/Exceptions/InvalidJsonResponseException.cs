﻿using System;

namespace lkcode.hetznercloudapi.Exceptions
{
    public class InvalidJsonResponseException : Exception
    {
        public InvalidJsonResponseException()
        {
        }

        public InvalidJsonResponseException(string message)
            : base(message)
        {
        }

        public InvalidJsonResponseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
