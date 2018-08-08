using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Api
{
    public class Network
    {
        public AddressIpv4 Ipv4 { get; set; }
        
        public AddressIpv6 Ipv6 { get; set; }

        private List<int> _floatingIpIds { get; set; }
        public List<int> FloatingIpIds {
            get
            {
                return this._floatingIpIds;
            }
            set
            {
                this._floatingIpIds = value;

                this.LoadFloatingIpIds();
            }
        }

        public List<FloatingIp> FloatingIps { get; set; }

        private void LoadFloatingIpIds()
        {
            this.FloatingIps = new List<FloatingIp>();

            foreach (int floatingIpId in this.FloatingIpIds)
            {
                this.FloatingIps.Add(new FloatingIp(floatingIpId));
            }
        }
    }

    public class AddressIpv6
    {
        public string Ip { get; set; }
        
        public bool Blocked { get; set; }
    }

    public class AddressIpv4
    {
        public string Ip { get; set; }
        
        public bool Blocked { get; set; }
    }
}