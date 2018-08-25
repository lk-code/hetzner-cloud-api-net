using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class ServerType
    {
        #region # static properties #

        private static int _currentPage { get; set; }
        public static int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        private static int _maxPages { get; set; }
        public static int MaxPages
        {
            get
            {
                return _maxPages;
            }
            set
            {
                _maxPages = value;
            }
        }

        #endregion

        #region # public properties #

        /// <summary>
        /// ID of the server type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier of the server type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the server type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number of cpu cores a server of this type will have.
        /// </summary>
        public int Cores { get; set; }

        /// <summary>
        /// Memory a server of this type will have in GB.
        /// </summary>
        public double Memory { get; set; }

        /// <summary>
        /// Disk size a server of this type will have in GB.
        /// </summary>
        public int Disc { get; set; }

        /// <summary>
        /// Type of server boot drive. Local has higher speed. Network has better availability..
        /// </summary>
        public string StorageType { get; set; }

        /// <summary>
        /// Type of cpu.
        /// </summary>
        public string CpuType { get; set; }

        /// <summary>
        /// Prices in different ServerTypes.
        /// </summary>
        public List<ServerTypePricingValue> Prices { get; set; }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all datacenter in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ServerType>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<ServerType> serverTypesList = new List<ServerType>();

            string url = string.Format("/server_types");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.ServerType.Get.Response response = JsonConvert.DeserializeObject<Objects.ServerType.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.ServerType.Universal.ServerType responseServerType in response.server_types)
            {
                ServerType serverType = GetServerTypeFromResponseData(responseServerType);

                serverTypesList.Add(serverType);
            }

            return serverTypesList;
        }

        /// <summary>
        /// Returns all datacenter with the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<ServerType> GetAsync(long id)
        {
            ServerType serverType = new ServerType();

            string url = string.Format("/server_types/{0}", id);

            string responseContent = await ApiCore.SendRequest(url);
            Objects.ServerType.GetOne.Response response = JsonConvert.DeserializeObject<Objects.ServerType.GetOne.Response>(responseContent);

            serverType = GetServerTypeFromResponseData(response.server_type);

            return serverType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public static ServerTypePricing GetServerTypePricingFromResponseData(Objects.Pricing.Get.ServerType serverType)
        {
            ServerTypePricing stp = new ServerTypePricing();

            stp.Id = serverType.id;
            stp.Name = serverType.name;
            stp.Prices = new List<ServerTypePricingValue>();

            foreach (Objects.ServerType.Universal.Price serverTypePrice in serverType.prices)
            {
                ServerTypePricingValue stpv = ServerType.GetServerTypePricingValueFromResponseData(serverTypePrice);

                stp.Prices.Add(stpv);
            }

            return stp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverTypePrice"></param>
        /// <returns></returns>
        public static ServerTypePricingValue GetServerTypePricingValueFromResponseData(Objects.ServerType.Universal.Price serverTypePrice)
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
            return stpv;
        }

        #endregion

        #region # public methods #

        public ServerType()
        {

        }

        public ServerType(int id)
        {
            this.Id = id;
        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static ServerType GetServerTypeFromResponseData(Objects.ServerType.Universal.ServerType responseData)
        {
            ServerType serverType = new ServerType();

            serverType.Id = responseData.id;
            serverType.Name = responseData.name;
            serverType.Description = responseData.description;
            serverType.Cores = responseData.cores;
            serverType.CpuType = responseData.cpu_type;
            serverType.Disc = responseData.disk;
            serverType.Memory = responseData.memory;
            serverType.StorageType = responseData.storage_type;

            serverType.Prices = new List<ServerTypePricingValue>();

            foreach (Objects.ServerType.Universal.Price serverTypePrice in responseData.prices)
            {
                ServerTypePricingValue stpv = ServerType.GetServerTypePricingValueFromResponseData(serverTypePrice);

                serverType.Prices.Add(stpv);
            }

            return serverType;
        }

        #endregion
    }
}
