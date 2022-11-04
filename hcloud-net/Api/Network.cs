using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Api
{
    /// <summary>
    /// Public network information.
    /// </summary>
    public class Network
    {
        /// <summary>
        /// IP address (v4) and its reverse dns entry of this server.
        /// </summary>
        public AddressIpv4 Ipv4 { get; set; }

        /// <summary>
        /// IPv6 network assigned to this server and its reverse dns entry.
        /// </summary>
        public AddressIpv6 Ipv6 { get; set; }

        private List<int> _floatingIpIds { get; set; }
        /// <summary>
        /// IDs of floating IPs assigned to this server.
        /// </summary>
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

        /// <summary>
        /// List of floating IPs assigned to this server.
        /// </summary>
        public List<FloatingIp> FloatingIps { get; set; }

        private void LoadFloatingIpIds()
        {
            this.FloatingIps = null;
            this.FloatingIps = new List<FloatingIp>();

            foreach (int floatingIpId in this.FloatingIpIds)
            {
                this.FloatingIps.Add(new FloatingIp(floatingIpId));
            }
        }
    }

    /// <summary>
    /// IP address (v4) and its reverse dns entry of this server.
    /// </summary>
    public class AddressIpv4
    {
        /// <summary>
        /// IP address (v4) of this server.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// If the IP is blocked by our anti abuse dept.
        /// </summary>
        public bool Blocked { get; set; }

        /// <summary>
        /// Reverse DNS PTR entry for the IPv4 addresses of this server.
        /// </summary>
        public string DnsPointer { get; set; }
    }

    /// <summary>
    /// IPv6 network assigned to this server and its reverse dns entry.
    /// </summary>
    public class AddressIpv6
    {
        /// <summary>
        /// IP address (v6) of this server.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// If the IP is blocked by our anti abuse dept.
        /// </summary>
        public bool Blocked { get; set; }

        /// <summary>
        /// Reverse DNS PTR entries for the IPv6 addresses of this server, `null` by default.
        /// </summary>
        public List<string> DnsPointer { get; set; }
    }
}