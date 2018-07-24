using System;
using System.Collections.Generic;

namespace CloudApiNet.Objects.Server.GetOne
{
    public class Ipv4
    {
        public string ip { get; set; }
        public bool blocked { get; set; }
        public string dns_ptr { get; set; }
    }

    public class Ipv6
    {
        public string ip { get; set; }
        public bool blocked { get; set; }
        public List<object> dns_ptr { get; set; }
    }

    public class PublicNet
    {
        public Ipv4 ipv4 { get; set; }
        public Ipv6 ipv6 { get; set; }
        public List<object> floating_ips { get; set; }
    }

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

    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class ServerTypes
    {
        public List<int> supported { get; set; }
        public List<int> available { get; set; }
    }

    public class Datacenter
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Location location { get; set; }
        public ServerTypes server_types { get; set; }
    }

    public class Protection
    {
        public bool delete { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
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
    }

    public class Protection2
    {
        public bool delete { get; set; }
        public bool rebuild { get; set; }
    }

    public class Server
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public PublicNet public_net { get; set; }
        public ServerType server_type { get; set; }
        public Datacenter datacenter { get; set; }
        public Image image { get; set; }
        public object iso { get; set; }
        public bool rescue_enabled { get; set; }
        public bool locked { get; set; }
        public object backup_window { get; set; }
        public int outgoing_traffic { get; set; }
        public int ingoing_traffic { get; set; }
        public long included_traffic { get; set; }
        public Protection2 protection { get; set; }
    }

    public class Response
    {
        public Server server { get; set; }
    }
}
