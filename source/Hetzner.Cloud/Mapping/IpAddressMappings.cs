using System.Text.Json;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class IpAddressMappings
{
    internal static Ip4Address ToIp4Address(this JsonElement json)
    {
        Ip4Address data = new(json.GetProperty("id").GetInt64(),
            json.GetProperty("ip").GetString()!,
            json.GetProperty("blocked").GetBoolean(),
            json.GetProperty("dns_ptr").GetString()!);

        return data;
    }
    
    internal static Ip6Address ToIp6Address(this JsonElement json)
    {
        Ip6Address data = new(json.GetProperty("id").GetInt64(),
            json.GetProperty("ip").GetString()!,
            json.GetProperty("blocked").GetBoolean(),
            json.GetProperty("dns_ptr").ToIpDnsPointers());

        return data;
    }
    
    internal static IpDnsPointer[] ToIpDnsPointers(this JsonElement json)
    {
        IpDnsPointer[] data = json.EnumerateArray()
            .Select(x => x.ToIpDnsPointer())
            .ToArray();

        return data;
    }
    
    internal static IpDnsPointer ToIpDnsPointer(this JsonElement json)
    {
        IpDnsPointer data = new(json.GetProperty("ip").GetString()!,
            json.GetProperty("dns_ptr").GetString()!);

        return data;
    }
}