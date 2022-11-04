using System;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Api
{
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public class ServerMetricTimeSeries
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> CpuValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricDiskValues> DiskValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricNetworkValues> NetworkValues { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServerMetricNetworkValues
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> PPSIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> PPSOut { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> BandwithIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> BandwithOut { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServerMetricDiskValues
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> IOPSRead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> IOPSWrite { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> BandwithRead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ServerMetricValue> BandwithWrite { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServerMetricValue
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
