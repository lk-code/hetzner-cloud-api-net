using lkcode.hetznercloudapi.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class FloatingIp
    {
        private bool _isInitialized { get; set; }
        /// <summary>
        /// If false, this floating-ip is not loaded (only the object from the server). access a field like floatingIp.Description and the object will load the data in the background.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this._isInitialized;
            }
        }

        private int _id { get; set; }
        /// <summary>
        /// ID of the Floating IP.
        /// </summary>
        public int Id
        {
            get
            {
                return this._id;
            }
        }

        private string _description { get; set; }
        /// <summary>
        /// Description of the Floating IP.
        /// </summary>
        public string Description
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        private string _ip { get; set; }
        /// <summary>
        /// IP address of the Floating IP.
        /// </summary>
        public string Ip
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }

        private string _type { get; set; }
        /// <summary>
        /// Type of the Floating IP.
        /// </summary>
        public string Type
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        private long? _serverId { get; set; }
        /// <summary>
        /// Id of the Server the Floating IP is assigned to, null if it is not assigned at all.
        /// </summary>
        public long? ServerId
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._serverId;
            }
            set
            {
                this._serverId = value;
            }
        }

        private bool _blocked { get; set; }
        /// <summary>
        /// Whether the IP is blocked.
        /// </summary>
        public bool Blocked
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._blocked;
            }
            set
            {
                this._blocked = value;
            }
        }

        private FloatingIpProtection _protection { get; set; }
        /// <summary>
        /// Protection configuration for the Floating IP.
        /// </summary>
        public FloatingIpProtection Protection
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._protection;
            }
            set
            {
                this._protection = value;
            }
        }

        private Location _homeLocation { get; set; }
        /// <summary>
        /// Location the Floating IP was created in. Routing is optimized for this location.
        /// </summary>
        public Location HomeLocation
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._homeLocation;
            }
            set
            {
                this._homeLocation = value;
            }
        }

        private List<FloatingIpDnsPointer> _dnsPointer { get; set; }
        /// <summary>
        /// Array of reverse DNS entries.
        /// </summary>
        public List<FloatingIpDnsPointer> DnsPointer
        {
            get
            {
                this.LoadData(); // load data if not initialized

                return this._dnsPointer;
            }
            set
            {
                this._dnsPointer = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public FloatingIp(int id)
        {
            this._id = id;
        }

        #region # public methods #
        
        /// <summary>
        /// Returns a floating-id by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<FloatingIp> GetAsync(long id)
        {
            string responseContent = await ApiCore.SendRequest(string.Format("/floating_ips/{0}", id.ToString()));
            Objects.FloatingIp.GetOne.Response response = JsonConvert.DeserializeObject<Objects.FloatingIp.GetOne.Response>(responseContent);

            FloatingIp floatingIp = GetFloatingIpFromResponseData(response.floating_ip);

            return floatingIp;
        }

        /// <summary>
        /// Deletes the current floating-ip.
        /// </summary>
        public async Task DeleteAsync()
        {
            this.LoadData(); // load data if not initialized

            await ApiCore.SendDeleteRequest(string.Format("/floating_ips/{0}", this.Id.ToString()));
        }

        #endregion

        #region # private methods #

        /// <summary>
        /// loads the data for this floating-ip.
        /// </summary>
        private async void LoadData()
        {
            if(this.IsInitialized)
            {
                return;
            }

            FloatingIp floatingIp = await GetAsync(this.Id);

            this.Description = floatingIp.Description;
            this.Ip = floatingIp.Ip;
            this.Type = floatingIp.Type;
            this.ServerId = floatingIp.ServerId;
            this.Blocked = floatingIp.Blocked;
            this.Protection = floatingIp.Protection;
            this.DnsPointer = floatingIp.DnsPointer;
            this.HomeLocation = floatingIp.HomeLocation;
            this._isInitialized = true;
        }

        /// <summary>
        /// returns a FloatingIp-Object for the given data
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static FloatingIp GetFloatingIpFromResponseData(Objects.FloatingIp.GetOne.FloatingIp responseData)
        {
            FloatingIp floatingIp = new FloatingIp(responseData.id);

            floatingIp._isInitialized = true;
            floatingIp.Description = responseData.description;
            floatingIp.Ip = responseData.ip;
            floatingIp.Type = responseData.type;
            floatingIp.ServerId = responseData.server;
            floatingIp.Blocked = responseData.blocked;

            floatingIp.Protection = new FloatingIpProtection()
            {
                Delete = responseData.protection.delete
            };

            floatingIp.HomeLocation = new Location()
            {
                Id = responseData.home_location.id,
                Name = responseData.home_location.name,
                City = responseData.home_location.city,
                Country = responseData.home_location.country,
                Description = responseData.home_location.description,
                Latitude = responseData.home_location.latitude,
                Longitude = responseData.home_location.longitude,
            };

            floatingIp.DnsPointer = new List<FloatingIpDnsPointer>();
            foreach(var dnsPtr in responseData.dns_ptr)
            {
                floatingIp.DnsPointer.Add(new FloatingIpDnsPointer()
                {
                    Ip = dnsPtr.ip,
                    DnsPointer = dnsPtr.dns_ptr
                });
            }

            return floatingIp;
        }

        #endregion
    }
}