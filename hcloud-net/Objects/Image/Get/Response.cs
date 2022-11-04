using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Image.Get
{
    public class Response
    {
        public List<Image.Universal.Image> images { get; set; }
        public Meta meta { get; set; }
    }
}