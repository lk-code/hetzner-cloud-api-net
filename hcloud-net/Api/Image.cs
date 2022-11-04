using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// Type of the image. Choices: system, snapshot, backup.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Whether the image can be used or if it’s still being created. Choices: available, creating.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Unique identifier of the image. This value is only set for system images.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Size of the image file in our storage in GB. For snapshot images this is the value relevant for calculating costs for the image.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Description of the image.
        /// </summary>
        public double? ImageSize { get; set; } = 0;

        /// <summary>
        /// Size of the disk contained in the image in GB.
        /// </summary>
        public double? DiskSize { get; set; } = 0;

        private DateTimeOffset _created { get; set; }
        /// <summary>
        /// Point in time when the image was created (in ISO-8601 format) as a System.DateTimeOffset.
        /// </summary>
        public DateTimeOffset Created
        {
            get
            {
                return _created;
            }
        }

        /// <summary>
        /// Information about the server the image was created from.
        /// </summary>
        public Server CreatedFrom { get; set; } = null;

        /// <summary>
        /// ID of server the image is bound to. Only set for images of type backup.
        /// </summary>
        public Server BoundTo { get; set; } = null;

        /// <summary>
        /// Flavor of operating system contained in the image Choices: ubuntu, centos, debian, fedora, unknown.
        /// </summary>
        public string OsFlavor { get; set; } = string.Empty;

        /// <summary>
        /// Operating system version.
        /// </summary>
        public string OsVersion { get; set; } = string.Empty;

        /// <summary>
        /// Indicates that rapid deploy of the image is available.
        /// </summary>
        public bool RapidDeploy { get; set; } = false;

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
            Image image = new Image();

            image.Id = responseData.id;
            image.Type = responseData.type;
            image.Status = responseData.status;
            image.Name = responseData.name;
            image.Description = responseData.description;
            image.ImageSize = responseData.image_size;
            image.DiskSize = responseData.disk_size;
            image._created = responseData.created;
            image.OsFlavor = responseData.os_flavor;
            image.OsVersion = responseData.os_version;
            image.RapidDeploy = responseData.rapid_deploy;

            if(responseData.bound_to.HasValue)
            {
                image.BoundTo = new Server()
                {
                    Id = responseData.bound_to.Value
                };
            }

            return image;
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
