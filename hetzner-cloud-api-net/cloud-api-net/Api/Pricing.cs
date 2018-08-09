using lkcode.hetznercloudapi.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Pricing
    {
        /// <summary>
        /// 
        /// </summary>
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string VatRate { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ImagePricing Image { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public FloatingIpPricing FloatingIp { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public TrafficPricing Traffic { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public ServerBackupPricing ServerBackup { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public List<ServerTypePricing> ServerTypes { get; set; } = null;

        #region # static methods #

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<Pricing> GetAsync()
        {
            Pricing pricings = new Pricing();

            string responseContent = await ApiCore.SendRequest("/pricing");
            Objects.Pricing.Get.Response response = JsonConvert.DeserializeObject<Objects.Pricing.Get.Response>(responseContent);

            pricings = GetPricingsFromResponseData(response);

            return pricings;
        }

        #endregion

        #region # private methods #
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static Pricing GetPricingsFromResponseData(Objects.Pricing.Get.Response responseData)
        {
            Pricing pricings = new Pricing();

            pricings.Currency = responseData.pricing.currency;
            pricings.VatRate = responseData.pricing.vat_rate;
            pricings.Image = new ImagePricing()
            {
                PricePerGbMonth = new PricingValue()
                {
                    Net = responseData.pricing.image.price_per_gb_month.net,
                    Gross = responseData.pricing.image.price_per_gb_month.gross
                }
            };
            pricings.FloatingIp = new FloatingIpPricing()
            {
                PriceMontly = new PricingValue()
                {
                    Net = responseData.pricing.floating_ip.price_monthly.net,
                    Gross = responseData.pricing.floating_ip.price_monthly.gross
                }
            };
            pricings.Traffic = new TrafficPricing()
            {
                PricePerTb = new PricingValue()
                {
                    Net = responseData.pricing.traffic.price_per_tb.net,
                    Gross = responseData.pricing.traffic.price_per_tb.gross
                }
            };
            pricings.ServerBackup = new ServerBackupPricing()
            {
                Percentage = responseData.pricing.server_backup.percentage
            };
            pricings.ServerTypes = new List<ServerTypePricing>();

            foreach (var serverType in responseData.pricing.server_types)
            {
                ServerTypePricing stp = new ServerTypePricing();

                stp.Id = serverType.id;
                stp.Name = serverType.name;
                stp.Prices = new List<ServerTypePricingValue>();

                foreach(var serverTypePrice in serverType.prices)
                {
                    ServerTypePricingValue stpv = new ServerTypePricingValue();

                    stpv.Location = serverTypePrice.location;
                    stpv.PriceHourly = new PricingValue()
                    {
                        Net = serverTypePrice.price_hourly.net,
                        Gross = serverTypePrice.price_hourly.gross
                    };
                    stpv.PriceMontly = new PricingValue()
                    {
                        Net = serverTypePrice.price_monthly.net,
                        Gross = serverTypePrice.price_monthly.gross
                    };

                    stp.Prices.Add(stpv);
                }

                pricings.ServerTypes.Add(stp);
            }

            return pricings;
        }

        #endregion
    }
}