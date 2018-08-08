using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Pricing.Get
{
    public class PricePerGbMonth
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class Image
    {
        public PricePerGbMonth price_per_gb_month { get; set; }
    }

    public class PriceMonthly
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class FloatingIp
    {
        public PriceMonthly price_monthly { get; set; }
    }

    public class PricePerTb
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class Traffic
    {
        public PricePerTb price_per_tb { get; set; }
    }

    public class ServerBackup
    {
        public string percentage { get; set; }
    }

    public class PriceHourly
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class PriceMonthly2
    {
        public string net { get; set; }
        public string gross { get; set; }
    }

    public class Price
    {
        public string location { get; set; }
        public PriceHourly price_hourly { get; set; }
        public PriceMonthly2 price_monthly { get; set; }
    }

    public class ServerType
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Price> prices { get; set; }
    }

    public class Pricing
    {
        public string currency { get; set; }
        public string vat_rate { get; set; }
        public Image image { get; set; }
        public FloatingIp floating_ip { get; set; }
        public Traffic traffic { get; set; }
        public ServerBackup server_backup { get; set; }
        public List<ServerType> server_types { get; set; }
    }

    public class Response
    {
        public Pricing pricing { get; set; }
    }
}
