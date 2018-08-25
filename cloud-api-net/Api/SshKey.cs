using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class SshKey
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
        /// ID of the SSH key.
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// Name of the SSH key (must be unique per project).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Fingerprint of public key.
        /// </summary>
        public string Fingerprint { get; set; } = string.Empty;

        /// <summary>
        /// Public key.
        /// </summary>
        public string PublicKey { get; set; } = string.Empty;

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all ssh-keys in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<SshKey>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<SshKey> sshKeyList = new List<SshKey>();

            string url = string.Format("/ssh_keys");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.SshKey.Get.Response response = JsonConvert.DeserializeObject<Objects.SshKey.Get.Response>(responseContent);
            
            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.SshKey.Universal.SshKey responseSshKey in response.ssh_keys)
            {
                SshKey sshKey = GetSshKeyFromResponseData(responseSshKey);

                sshKeyList.Add(sshKey);
            }

            return sshKeyList;
        }

        /// <summary>
        /// Return a ssh-key by the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<SshKey> GetAsync(long id)
        {
            SshKey sshKey = new SshKey();

            string url = string.Format("/ssh_keys/{0}", id);

            string responseContent = await ApiCore.SendRequest(url);
            Objects.SshKey.GetOne.Response response = JsonConvert.DeserializeObject<Objects.SshKey.GetOne.Response>(responseContent);
            
            sshKey = GetSshKeyFromResponseData(response.ssh_key);

            return sshKey;
        }

        #endregion

        #region # public methods #

        public SshKey()
        {

        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static SshKey GetSshKeyFromResponseData(Objects.SshKey.Universal.SshKey responseData)
        {
            SshKey sshKey = new SshKey();
            
            sshKey.Id = responseData.id;
            sshKey.Name = responseData.name;
            sshKey.Fingerprint = responseData.fingerprint;
            sshKey.PublicKey = responseData.public_key;

            return sshKey;
        }

        #endregion
    }
}