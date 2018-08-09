namespace lkcode.hetznercloudapi.Api
{
    public class FloatingIpPricing
    {
        /// <summary>
        /// the cost of one floating IP per month.
        /// </summary>
        public PricingValue PriceMontly { get; set; } = new PricingValue();
    }
}
