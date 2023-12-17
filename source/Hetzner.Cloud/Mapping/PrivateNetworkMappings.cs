using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class PrivateNetworkMappings
{
    internal static PrivateNetwork[] ToPrivateNetworks(this JsonElement json)
    {
        PrivateNetwork[] data = json.EnumerateArray()
            .Select(x => x.ToPrivateNetwork())
            .ToArray();

        return data;
    }
    
    internal static PrivateNetwork ToPrivateNetwork(this JsonElement json)
    {
        PrivateNetwork data = new(json.GetProperty("ip").GetString()!,
            json.GetProperty("mac_address").GetString()!,
            json.GetProperty("network").GetInt64(),
            json.GetNullableProperty("alias_ips").ToStringArray());

        return data;
    }
}
