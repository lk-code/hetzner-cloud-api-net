using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.ServerType.Universal
{
    public class PriceHourly
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class PriceMonthly
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class Price
    {
        public string location { get; set; }
        public PriceHourly price_hourly { get; set; }
        public PriceMonthly price_monthly { get; set; }
    }

    public class ServerType
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int cores { get; set; }
        public double memory { get; set; }
        public int disk { get; set; }
        public List<Price> prices { get; set; }
        public string storage_type { get; set; }
        public string cpu_type { get; set; }
    }
}
