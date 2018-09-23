using System;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Action.Universal
{
    public class Resource
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Action
    {
        public int id { get; set; }
        public string command { get; set; }
        public string status { get; set; }
        public int progress { get; set; }
        public DateTime started { get; set; }
        public DateTime finished { get; set; }
        public List<Resource> resources { get; set; }
        public Error error { get; set; }
    }
}