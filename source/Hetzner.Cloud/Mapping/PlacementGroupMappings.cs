using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class PlacementGroupMappings
{
    internal static PlacementGroup? ToPlacementGroup(this JsonElement serverJson)
    {
        if (serverJson.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        PlacementGroup placementGroup = new(serverJson.GetProperty("id").GetInt64())
        {
            Name = serverJson.GetProperty("name").GetString()!,
            Created = DateTime.Parse(serverJson.GetProperty("created").GetString()!),
            Labels = serverJson.GetProperty("labels").ToDictionary(),
            Type = serverJson.GetProperty("type").GetString()!,
            ServerIds = serverJson.GetNullableProperty("servers").ToLongArray(),
        };

        return placementGroup;
    }
}
