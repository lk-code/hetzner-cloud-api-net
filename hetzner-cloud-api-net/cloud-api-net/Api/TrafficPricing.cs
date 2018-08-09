namespace lkcode.hetznercloudapi.Api
{
    public class TrafficPricing
    {
        /// <summary>
        /// the cost of additional traffic per TB.
        /// </summary>
        public PricingValue PricePerTb { get; set; } = new PricingValue();
    }
}