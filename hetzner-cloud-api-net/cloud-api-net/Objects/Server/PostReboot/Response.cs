using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApiNet.Objects.Server.PostReboot
{
    public class Resource
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class Action
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

    public class Response
    {
        public Action action { get; set; }
    }
}