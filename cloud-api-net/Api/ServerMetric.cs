using System;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Api
{
    public class ServerMetric
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ServerMetricTimeSeries TimeSeries { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
    }

    public class ServerMetricTimeSeries
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricCpuValue> CpuValues { get; set; }
    }

    public class ServerMetricCpuValue
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long TimestampValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }
    }
}
