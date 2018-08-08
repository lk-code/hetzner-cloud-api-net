using System;

namespace lkcode.hetznercloudapi.Api
{
    public class ServerActionResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; } = 0;

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

        /// <summary>
        /// 
        /// </summary>
        public object AdditionalActionContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Error Error { get; set; }
    }
}
