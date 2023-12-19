using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class PlacementGroupMappings
{
    internal static PlacementGroup? ToPlacementGroup(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        PlacementGroup data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
            Created = DateTime.Parse(json.GetProperty("created").GetString()!),
            Labels = json.GetProperty("labels").ToDictionary(),
            Type = json.GetProperty("type").GetString()!,
            ServerIds = json.GetNullableProperty("servers").ToLongArray(),
        };

        return data;
    }
}
