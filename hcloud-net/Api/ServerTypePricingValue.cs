namespace lkcode.hetznercloudapi.Api
{
    public class ServerTypePricingValue
    {
        /// <summary>
        /// Name of the location the price is for.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Hourly costs for a server type in this location.
        /// </summary>
        public PricingValue PriceHourly { get; set; } = null;

        /// <summary>
        /// Monthly costs for a server type in this location.
        /// </summary>
        public PricingValue PriceMontly { get; set; } = null;
    }
}
