using System;

namespace lkcode.hetznercloudapi.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidArgumentException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public InvalidArgumentException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InvalidArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public InvalidArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}