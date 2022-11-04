using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Action.Get
{
    public class Response
    {
        public List<Action.Universal.Action> actions { get; set; }
        public Meta meta { get; set; }
    }
}