namespace lkcode.hetznercloudapi.Api
{
    public class ImagePricing
    {
        /// <summary>
        /// the cost of one 1GB Image for the full month.
        /// </summary>
        public PricingValue PricePerGbMonth { get; set; } = new PricingValue();
    }
}
