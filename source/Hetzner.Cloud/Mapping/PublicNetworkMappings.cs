using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class PublicNetworkMappings
{
    internal static PublicNetwork ToPublicNetwork(this JsonElement json)
    {
        PublicNetwork data = new()
        {
            Firewalls = json.GetProperty("firewalls").ToFirewalls(),
            FloatingIps = json.GetNullableProperty("floating_ips").ToLongArray()!,
            Ipv4 = json.GetProperty("ipv4").ToIp4Address(),
            Ipv6 = json.GetProperty("ipv6").ToIp6Address(),
        };

        return data;
    }
}
