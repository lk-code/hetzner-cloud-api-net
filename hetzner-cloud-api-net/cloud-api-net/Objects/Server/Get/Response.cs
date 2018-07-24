using System.Collections.Generic;

namespace CloudApiNet.Objects.Server.Get
{
    public class Pagination
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public object previous_page { get; set; }
        public object next_page { get; set; }
        public int last_page { get; set; }
        public int total_entries { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Response
    {
        public List<Server.Universal.Server> servers { get; set; }
        public Meta meta { get; set; }
    }
}
