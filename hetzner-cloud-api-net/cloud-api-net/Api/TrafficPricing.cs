namespace lkcode.hetznercloudapi.Api
{
    public class TrafficPricing
    {
        /// <summary>
        /// The cost of additional traffic per GB.
        /// </summary>
        public PricingValue PricePerTb { get; set; } = new PricingValue();
    }
}