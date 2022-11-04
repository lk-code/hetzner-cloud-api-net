using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.ServerType.Get
{

    public class Response
    {
        public List<Universal.ServerType> server_types { get; set; }
        public Meta meta { get; set; }
    }
}
