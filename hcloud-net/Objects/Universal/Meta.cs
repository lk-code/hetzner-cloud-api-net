namespace lkcode.hetznercloudapi.Objects.Universal
{
    public class Pagination
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int? previous_page { get; set; }
        public int? next_page { get; set; }
        public int last_page { get; set; }
        public int total_entries { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }
}
