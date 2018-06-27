using CloudApiNet.ApiClients;
using CloudApiNet.Objects.NetworkObjects;
using System;

namespace CloudApiNet.Objects.ServerObjects
{
    public class Server : ApiObject
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Network Network { get; set; }

        public void ChangeName(string name)
        {

        }
    }
}