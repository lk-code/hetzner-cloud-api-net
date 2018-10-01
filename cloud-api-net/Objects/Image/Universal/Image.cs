using System;

namespace lkcode.hetznercloudapi.Objects.Image.Universal
{
    public class Protection
    {
        public bool delete { get; set; }
    }

    public class Labels
    {
    }

    public class Image
    {
        public long id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object image_size { get; set; }
        public int disk_size { get; set; }
        public DateTime created { get; set; }
        public object created_from { get; set; }
        public object bound_to { get; set; }
        public string os_flavor { get; set; }
        public string os_version { get; set; }
        public bool rapid_deploy { get; set; }
        public Protection protection { get; set; }
        public object deprecated { get; set; }
        public Labels labels { get; set; }
    }
}
