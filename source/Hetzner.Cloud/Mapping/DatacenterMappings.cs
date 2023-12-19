using System.Text.Json;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class DatacenterMappings
{
    internal static Datacenter? ToDatacenter(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        Datacenter data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
            Description = json.GetProperty("description").GetString()!,
            Location = json.GetProperty("location").ToLocation(),
            ServerTypes = json.GetProperty("server_types").ToServerTypes(),
        };

        return data;
    }
}
