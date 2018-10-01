using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Image
    {
        #region # static properties #

        private static int _currentPage { get; set; }
        /// <summary>
        /// The current selected page.
        /// </summary>
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
        /// <summary>
        /// The number of pages with server.
        /// </summary>
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
        /// ID of image.
        /// </summary>
        public long Id { get; set; } = 0;

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all images.
        /// </summary>
        /// <param name="page">optional parameter to get a specific page. default is page 1.</param>
        /// <returns>a list with the image-objects.</returns>
        public static async Task<List<Image>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<Image> imageList = new List<Image>();

            string url = string.Format("/images");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Image.Get.Response response = JsonConvert.DeserializeObject<Objects.Image.Get.Response>(responseContent);

            // load meta
            SaveResponseMetaData(response);

            foreach (Objects.Image.Universal.Image responseServer in response.images)
            {
                Image image = GetImageFromResponseData(responseServer);

                imageList.Add(image);
            }

            return imageList;
        }

        #endregion

        #region # public methods #



        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static Image GetImageFromResponseData(Objects.Image.Universal.Image responseData)
        {
            Image server = new Image();

            server.Id = responseData.id;
            server.Name = responseData.name;
            server.Status = responseData.status;
            server._created = responseData.created;
            server.Network = new Network()
            {
                Ipv4 = new AddressIpv4()
                {
                    Ip = responseData.public_net.ipv4.ip,
                    Blocked = responseData.public_net.ipv4.blocked
                },
                Ipv6 = new AddressIpv6()
                {
                    Ip = responseData.public_net.ipv6.ip,
                    Blocked = responseData.public_net.ipv6.blocked
                },
                FloatingIpIds = responseData.public_net.floating_ips
            };

            return server;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private static void SaveResponseMetaData(Objects.Image.Get.Response response)
        {
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);
        }

        #endregion
    }
}
