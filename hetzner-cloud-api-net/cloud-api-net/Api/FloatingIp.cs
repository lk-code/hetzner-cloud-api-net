using lkcode.hetznercloudapi.Core;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class FloatingIp
    {
        private bool _isInitialized { get; set; }
        public bool IsInitialized
        {
            get
            {
                return this._isInitialized;
            }
        }

        private int _id { get; set; }
        public int Id {
            get {
                return this._id;
            }
        }

        public FloatingIp(int id)
        {
            this._id = id;
        }

        #region # public methods #
        
        public static async Task<FloatingIp> GetAsync(long id)
        {
            string responseContent = await ApiCore.SendRequest(string.Format("/floating_ips/{0}", id.ToString()));
            Objects.Server.GetOne.Response response = JsonConvert.DeserializeObject<Objects.Server.GetOne.Response>(responseContent);

            //FloatingIp floatingIp = GetFloatingIpFromResponseData(response.server);

            FloatingIp floatingIp = new FloatingIp(1);

            return floatingIp;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task DeleteAsync()
        {
            this.LoadData(); // load data if not initialized

            await ApiCore.SendDeleteRequest(string.Format("/floating_ips/{0}", this.Id.ToString()));
        }

        #endregion

        #region # private methods #

        private async void LoadData()
        {
            if(this.IsInitialized)
            {
                return;
            }

            FloatingIp floatingIp = await GetAsync(this.Id);
        }

        #endregion
    }
}