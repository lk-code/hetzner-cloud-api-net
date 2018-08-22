using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.FloatingIp.GetOne
{
    public class DnsPtr
    {
        public string ip { get; set; }
        public string dns_ptr { get; set; }
    }

    public class HomeLocation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Protection
    {
        public bool delete { get; set; }
    }

    public class FloatingIp
    {
        public int id { get; set; }
        public string description { get; set; }
        public string ip { get; set; }
        public string type { get; set; }
        public int server { get; set; }
        public List<DnsPtr> dns_ptr { get; set; }
        public HomeLocation home_location { get; set; }
        public bool blocked { get; set; }
        public Protection protection { get; set; }
    }

    public class Response
    {
        public FloatingIp floating_ip { get; set; }
    }
}
