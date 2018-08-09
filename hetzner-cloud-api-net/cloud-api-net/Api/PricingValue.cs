namespace lkcode.hetznercloudapi.Api
{
    public class PricingValue
    {
        /// <summary>
        /// the net price-value
        /// </summary>
        public string Net { get; set; } = string.Empty;

        /// <summary>
        /// the gross price-value
        /// </summary>
        public string Gross { get; set; } = string.Empty;
    }
}
