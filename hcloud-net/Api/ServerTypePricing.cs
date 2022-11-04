using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Api
{
    public class ServerTypePricing
    {
        /// <summary>
        /// ID of the server type the price is for.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Name of the server type the price is for.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Server type costs per location.
        /// </summary>
        public List<ServerTypePricingValue> Prices { get; set; } = null;
    }
}
