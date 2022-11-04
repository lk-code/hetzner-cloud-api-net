using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Server.Get
{

    public class Response
    {
        public List<Server.Universal.Server> servers { get; set; }
        public Meta meta { get; set; }
    }
}
