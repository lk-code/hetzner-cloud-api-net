namespace lkcode.hetznercloudapi.Api
{
    public class PricingValue
    {
        /// <summary>
        /// Price without VAT.
        /// </summary>
        public string Net { get; set; } = string.Empty;

        /// <summary>
        /// Price with VAT added.
        /// </summary>
        public string Gross { get; set; } = string.Empty;
    }
}
