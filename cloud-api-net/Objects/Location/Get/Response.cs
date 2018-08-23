using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Location.Get
{

    public class Response
    {
        public List<Universal.Location> locations { get; set; }
        public Meta meta { get; set; }
    }
}
