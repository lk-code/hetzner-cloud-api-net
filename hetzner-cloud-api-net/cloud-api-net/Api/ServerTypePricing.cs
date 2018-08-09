using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Api
{
    public class ServerTypePricing
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public List<ServerTypePricingValue> Prices { get; set; } = null;
    }
}
