using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class FirewallMappings
{
    internal static Firewall[] ToFirewalls(this JsonElement json)
    {
        Firewall[] data = json.EnumerateArray()
            .Select(x => x.ToFirewall())
            .ToArray();

        return data;
    }

    internal static Firewall ToFirewall(this JsonElement json)
    {
        Firewall data = new(json.GetProperty("id").GetInt64(),
            json.GetProperty("status").ToEnum<FirewallStatus>());

        return data;
    }
}
