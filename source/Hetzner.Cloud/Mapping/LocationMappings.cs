using System.Text.Json;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class LocationMappings
{
    internal static Location? ToLocation(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        Location data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
            Description = json.GetProperty("description").GetString()!,
            City = json.GetProperty("city").GetString()!,
            Country = json.GetProperty("country").GetString()!,
            Latitude = json.GetProperty("latitude").GetDouble(),
            Longitude = json.GetProperty("longitude").GetDouble(),
            NetworkZone = json.GetProperty("network_zone").GetString()!,
        };

        return data;
    }
}
