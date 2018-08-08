using lkcode.hetznercloudapi.Core;
using Newtonsoft.Json;
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

            return pricings;
        }

        #endregion
    }
}