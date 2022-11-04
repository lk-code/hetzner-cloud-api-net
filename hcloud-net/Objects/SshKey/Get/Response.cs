using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.SshKey.Get
{
    public class Response
    {
        public List<Universal.SshKey> ssh_keys { get; set; }
        public Meta meta { get; set; }
    }
}