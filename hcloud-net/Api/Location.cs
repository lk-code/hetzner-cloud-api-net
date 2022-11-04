using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Location
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
        /// ID of the location.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Unique identifier of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the location.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 code of the country the location resides in.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// City the location is closest to.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Latitude of the city closest to the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of the city closest to the location.
        /// </summary>
        public double Longitude { get; set; }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all datacenter in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Location>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<Location> locationsList = new List<Location>();

            string url = string.Format("/locations");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Location.Get.Response response = JsonConvert.DeserializeObject<Objects.Location.Get.Response>(responseContent);
            
            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.Location.Universal.Location responseDatacenter in response.locations)
            {
                Location location = GetLocationFromResponseData(responseDatacenter);

                locationsList.Add(location);
            }

            return locationsList;
        }

        /// <summary>
        /// Returns all datacenter with the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<Location> GetAsync(long id)
        {
            Location location = new Location();
            
            string url = string.Format("/locations/{0}", id);

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Location.GetOne.Response response = JsonConvert.DeserializeObject<Objects.Location.GetOne.Response>(responseContent);

            location = GetLocationFromResponseData(response.location);

            return location;
        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static Location GetLocationFromResponseData(Objects.Location.Universal.Location responseData)
        {
            Location location = new Location();

            location.Id = responseData.id;
            location.Name = responseData.name;
            location.Description = responseData.description;
            location.Country = responseData.country;
            location.City = responseData.city;
            location.Latitude = responseData.latitude;
            location.Longitude = responseData.longitude;

            return location;
        }

        #endregion
    }
}
