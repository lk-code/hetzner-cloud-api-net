namespace lkcode.hetznercloudapi.Objects.Location.Universal
{
    public class Location
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
