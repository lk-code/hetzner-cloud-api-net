// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CloudApiNet.Objects.ServerObjects.Objects;
//
//    var getAllResponse = GetAllResponse.FromJson(jsonString);

namespace CloudApiNet.Objects.ServerObjects.Objects
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetAllResponse
    {
        [JsonProperty("servers", Required = Required.Always)]
        public List<ResponseServer> Servers { get; set; }

        [JsonProperty("meta", Required = Required.Always)]
        public ResponseMeta Meta { get; set; }
    }

    public partial class ResponseMeta
    {
        [JsonProperty("pagination", Required = Required.Always)]
        public ResponsePagination Pagination { get; set; }
    }

    public partial class ResponsePagination
    {
        [JsonProperty("page", Required = Required.Always)]
        public long Page { get; set; }

        [JsonProperty("per_page", Required = Required.Always)]
        public long PerPage { get; set; }

        [JsonProperty("previous_page", Required = Required.AllowNull)]
        public object PreviousPage { get; set; }

        [JsonProperty("next_page", Required = Required.AllowNull)]
        public object NextPage { get; set; }

        [JsonProperty("last_page", Required = Required.Always)]
        public long LastPage { get; set; }

        [JsonProperty("total_entries", Required = Required.Always)]
        public long TotalEntries { get; set; }
    }

    public partial class ResponseServer
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty("created", Required = Required.Always)]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("public_net", Required = Required.Always)]
        public ResponsePublicNet PublicNet { get; set; }

        [JsonProperty("server_type", Required = Required.Always)]
        public ResponseServerType ServerType { get; set; }

        [JsonProperty("datacenter", Required = Required.Always)]
        public ResponseDatacenter Datacenter { get; set; }

        [JsonProperty("image", Required = Required.Always)]
        public ResponseImage Image { get; set; }

        [JsonProperty("iso", Required = Required.AllowNull)]
        public object Iso { get; set; }

        [JsonProperty("rescue_enabled", Required = Required.Always)]
        public bool RescueEnabled { get; set; }

        [JsonProperty("locked", Required = Required.Always)]
        public bool Locked { get; set; }

        [JsonProperty("backup_window", Required = Required.AllowNull)]
        public object BackupWindow { get; set; }

        [JsonProperty("outgoing_traffic", Required = Required.Always)]
        public long OutgoingTraffic { get; set; }

        [JsonProperty("ingoing_traffic", Required = Required.Always)]
        public long IngoingTraffic { get; set; }

        [JsonProperty("included_traffic", Required = Required.Always)]
        public long IncludedTraffic { get; set; }

        [JsonProperty("protection", Required = Required.Always)]
        public ResponseServerProtection Protection { get; set; }
    }

    public partial class ResponseDatacenter
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("location", Required = Required.Always)]
        public ResponseLocation Location { get; set; }

        [JsonProperty("server_types", Required = Required.Always)]
        public ResponseServerTypes ServerTypes { get; set; }
    }

    public partial class ResponseLocation
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("country", Required = Required.Always)]
        public string Country { get; set; }

        [JsonProperty("city", Required = Required.Always)]
        public string City { get; set; }

        [JsonProperty("latitude", Required = Required.Always)]
        public double Latitude { get; set; }

        [JsonProperty("longitude", Required = Required.Always)]
        public double Longitude { get; set; }
    }

    public partial class ResponseServerTypes
    {
        [JsonProperty("supported", Required = Required.Always)]
        public List<long> Supported { get; set; }

        [JsonProperty("available", Required = Required.Always)]
        public List<long> Available { get; set; }
    }

    public partial class ResponseImage
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("image_size", Required = Required.AllowNull)]
        public object ImageSize { get; set; }

        [JsonProperty("disk_size", Required = Required.Always)]
        public long DiskSize { get; set; }

        [JsonProperty("created", Required = Required.Always)]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("created_from", Required = Required.AllowNull)]
        public object CreatedFrom { get; set; }

        [JsonProperty("bound_to", Required = Required.AllowNull)]
        public object BoundTo { get; set; }

        [JsonProperty("os_flavor", Required = Required.Always)]
        public string OsFlavor { get; set; }

        [JsonProperty("os_version", Required = Required.Always)]
        public string OsVersion { get; set; }

        [JsonProperty("rapid_deploy", Required = Required.Always)]
        public bool RapidDeploy { get; set; }

        [JsonProperty("protection", Required = Required.Always)]
        public ResponseImageProtection Protection { get; set; }

        [JsonProperty("deprecated", Required = Required.AllowNull)]
        public object Deprecated { get; set; }
    }

    public partial class ResponseImageProtection
    {
        [JsonProperty("delete", Required = Required.Always)]
        public bool Delete { get; set; }
    }

    public partial class ResponseServerProtection
    {
        [JsonProperty("delete", Required = Required.Always)]
        public bool Delete { get; set; }

        [JsonProperty("rebuild", Required = Required.Always)]
        public bool Rebuild { get; set; }
    }

    public partial class ResponsePublicNet
    {
        [JsonProperty("ipv4", Required = Required.Always)]
        public ResponseIpv4 Ipv4 { get; set; }

        [JsonProperty("ipv6", Required = Required.Always)]
        public ResponseIpv6 Ipv6 { get; set; }

        [JsonProperty("floating_ips", Required = Required.Always)]
        public List<object> FloatingIps { get; set; }
    }

    public partial class ResponseIpv4
    {
        [JsonProperty("ip", Required = Required.Always)]
        public string Ip { get; set; }

        [JsonProperty("blocked", Required = Required.Always)]
        public bool Blocked { get; set; }

        [JsonProperty("dns_ptr", Required = Required.Always)]
        public string DnsPtr { get; set; }
    }

    public partial class ResponseIpv6
    {
        [JsonProperty("ip", Required = Required.Always)]
        public string Ip { get; set; }

        [JsonProperty("blocked", Required = Required.Always)]
        public bool Blocked { get; set; }

        [JsonProperty("dns_ptr", Required = Required.Always)]
        public List<object> DnsPtr { get; set; }
    }

    public partial class ResponseServerType
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("cores", Required = Required.Always)]
        public long Cores { get; set; }

        [JsonProperty("memory", Required = Required.Always)]
        public long Memory { get; set; }

        [JsonProperty("disk", Required = Required.Always)]
        public long Disk { get; set; }

        [JsonProperty("prices", Required = Required.Always)]
        public List<ResponsePrice> Prices { get; set; }

        [JsonProperty("storage_type", Required = Required.Always)]
        public string StorageType { get; set; }

        [JsonProperty("cpu_type", Required = Required.Always)]
        public string CpuType { get; set; }
    }

    public partial class ResponsePrice
    {
        [JsonProperty("location", Required = Required.Always)]
        public string Location { get; set; }

        [JsonProperty("price_hourly", Required = Required.Always)]
        public ResponsePriceHourlyClass PriceHourly { get; set; }

        [JsonProperty("price_monthly", Required = Required.Always)]
        public ResponsePriceHourlyClass PriceMonthly { get; set; }
    }

    public partial class ResponsePriceHourlyClass
    {
        [JsonProperty("net", Required = Required.Always)]
        public string Net { get; set; }

        [JsonProperty("gross", Required = Required.Always)]
        public string Gross { get; set; }
    }

    public partial class GetAllResponse
    {
        public static GetAllResponse FromJson(string json) => JsonConvert.DeserializeObject<GetAllResponse>(json, CloudApiNet.Objects.ServerObjects.Objects.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GetAllResponse self) => JsonConvert.SerializeObject(self, CloudApiNet.Objects.ServerObjects.Objects.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
