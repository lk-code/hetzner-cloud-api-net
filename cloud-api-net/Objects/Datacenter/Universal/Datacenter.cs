using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Datacenter.Universal
{
    public class Location
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class ServerTypes
    {
        public List<int> supported { get; set; }
        public List<int> available { get; set; }
    }

    public class Datacenter
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Location location { get; set; }
        public ServerTypes server_types { get; set; }
    }
}
