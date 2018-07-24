using System;
using System.Collections.Generic;

namespace HetznerCloudNet.Objects.Server.Universal
{
    public class Resource
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class ServerAction
    {
        public int id { get; set; }
        public string command { get; set; }
        public string status { get; set; }
        public int progress { get; set; }
        public DateTime started { get; set; }
        public object finished { get; set; }
        public List<Resource> resources { get; set; }
        public object error { get; set; }
    }
}
