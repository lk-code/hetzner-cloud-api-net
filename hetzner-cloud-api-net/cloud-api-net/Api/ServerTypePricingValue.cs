using System;
using System.Collections.Generic;
using System.Text;

namespace lkcode.hetznercloudapi.Api
{
    public class ServerTypePricingValue
    {
        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public PricingValue PriceHourly { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public PricingValue PriceMontly { get; set; } = null;
    }
}
