using lkcode.hetznercloudapi.Objects.Server.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Isos.Get
{
    public class Iso
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string deprecated { get; set; }
    }

    public class Response
    {
        public List<Iso> isos { get; set; }
        public Meta meta { get; set; }
    }
}
