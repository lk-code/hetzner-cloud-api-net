using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Datacenter
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
        /// ID of the datacenter.
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// Unique identifier of the datacenter.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description of the datacenter.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Location where the datacenter resides in.
        /// </summary>
        public Location Location { get; set; }

        private List<int> _supportedServerTypeIds { get; set; }
        /// <summary>
        /// IDs of server types that are supported in the datacenter.
        /// </summary>
        public List<int> SupportedServerTypeIds
        {
            get
            {
                return this._supportedServerTypeIds;
            }
            set
            {
                this._supportedServerTypeIds = value;

                this.LoadSupportedServerTypes();
            }
        }

        /// <summary>
        /// server types that are supported in the datacenter.
        /// </summary>
        public List<ServerType> SupportedServerTypes { get; set; }

        private void LoadSupportedServerTypes()
        {
            this.SupportedServerTypes = null;
            this.SupportedServerTypes = new List<ServerType>();

            foreach (int supportedServerTypeId in this.SupportedServerTypeIds)
            {
                this.SupportedServerTypes.Add(new ServerType(supportedServerTypeId));
            }
        }

        private List<int> _availableServerTypeIds { get; set; }
        /// <summary>
        /// IDs of server types that are available in the datacenter.
        /// </summary>
        public List<int> AvailableServerTypeIds
        {
            get
            {
                return this._availableServerTypeIds;
            }
            set
            {
                this._availableServerTypeIds = value;

                this.LoadAvailableServerTypes();
            }
        }

        /// <summary>
        /// server types that are available in the datacenter.
        /// </summary>
        public List<ServerType> AvailableServerTypes { get; set; }

        private void LoadAvailableServerTypes()
        {
            this.AvailableServerTypes = null;
            this.AvailableServerTypes = new List<ServerType>();

            foreach (int availableServerTypeId in this.AvailableServerTypeIds)
            {
                this.AvailableServerTypes.Add(new ServerType(availableServerTypeId));
            }
        }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all datacenter in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Datacenter>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<Datacenter> datacenterList = new List<Datacenter>();

            string url = string.Format("/datacenters");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Datacenter.Get.Response response = JsonConvert.DeserializeObject<Objects.Datacenter.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.Datacenter.Universal.Datacenter responseDatacenter in response.datacenters)
            {
                Datacenter server = GetDatacenterFromResponseData(responseDatacenter);

                datacenterList.Add(server);
            }

            return datacenterList;
        }

        /// <summary>
        /// Returns all datacenter with the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<Datacenter> GetAsync(long id)
        {
            Datacenter datacenter = new Datacenter();

            string url = string.Format("/datacenters/{0}", id);

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Datacenter.GetOne.Response response = JsonConvert.DeserializeObject<Objects.Datacenter.GetOne.Response>(responseContent);

            datacenter = GetDatacenterFromResponseData(response.datacenter);

            return datacenter;
        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static Datacenter GetDatacenterFromResponseData(Objects.Datacenter.Universal.Datacenter responseData)
        {
            Datacenter datacenter = new Datacenter();

            datacenter.Id = responseData.id;
            datacenter.Name = responseData.name;
            datacenter.Description = responseData.description;

            datacenter.Location = new Location()
            {
                Id = responseData.location.id,
                Name = responseData.location.name,
                City = responseData.location.city,
                Country = responseData.location.country,
                Description = responseData.location.description,
                Latitude = responseData.location.latitude,
                Longitude = responseData.location.longitude,
            };

            datacenter.SupportedServerTypeIds = responseData.server_types.supported;

            datacenter.AvailableServerTypeIds = responseData.server_types.available;

            return datacenter;
        }

        #endregion
    }
}
