using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApiNet.Objects.ServerObjects
{
    public class Server : ApiObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}