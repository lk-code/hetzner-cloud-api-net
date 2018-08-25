using System;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Server.GetMetrics
{
    public class Cpu
    {
        public List<List<object>> values { get; set; }
    }

    public class TimeSeries
    {
        public Cpu cpu { get; set; }
    }

    public class Metrics
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int step { get; set; }
        public TimeSeries time_series { get; set; }
    }

    public class Response
    {
        public Metrics metrics { get; set; }
    }
}
