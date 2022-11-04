using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class IsoImage
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
        /// 
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? Deprecated { get; set; } = null;

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all server in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<IsoImage>> GetAsync(int page = 1)
        {
            if((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<IsoImage> isoImageList = new List<IsoImage>();

            string url = string.Format("/isos");
            if(page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Isos.Get.Response response = JsonConvert.DeserializeObject<Objects.Isos.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            // load iso-images
            foreach (Objects.Isos.Get.Iso responseIsoImage in response.isos)
            {
                IsoImage isoImage = GetIsoImageFromResponseData(responseIsoImage);

                isoImageList.Add(isoImage);
            }

            return isoImageList;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<IsoImage> GetAsync(long id)
        {
            string responseContent = await ApiCore.SendRequest(string.Format("/isos/{0}", id.ToString()));
            Objects.Isos.Get.Iso response = JsonConvert.DeserializeObject<Objects.Isos.Get.Iso>(responseContent);

            IsoImage isoImage = GetIsoImageFromResponseData(response);

            return isoImage;
        }

        #endregion

        #region # private methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isoImage"></param>
        /// <returns></returns>
        private static IsoImage GetIsoImageFromResponseData(Objects.Isos.Get.Iso isoImage)
        {
            IsoImage image = new IsoImage();

            image.Id = isoImage.id;
            image.Name = isoImage.name;
            image.Type = isoImage.type;
            image.Description = isoImage.description;

            if(!string.IsNullOrEmpty(isoImage.deprecated) &&
                !string.IsNullOrWhiteSpace(isoImage.deprecated))
            {
                image.Deprecated = DateTime.Parse(isoImage.deprecated);
            }

            return image;
        }

        #endregion
    }
}
