using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApiNet.Api
{
    public class ServerActionResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int ActionId { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public int Progress { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public DateTime Started { get; set; }
    }
}
