using lkcode.hetznercloudapi.Objects.Server.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Datacenter.Get
{

    public class Response
    {
        public List<Universal.Datacenter> datacenters { get; set; }
        public int recommendation { get; set; }
        public Meta meta { get; set; }
    }
}
