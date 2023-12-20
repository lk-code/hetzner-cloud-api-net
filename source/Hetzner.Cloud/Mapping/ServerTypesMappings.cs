using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ServerTypesMappings
{
    internal static ServerTypes? ToServerTypes(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        ServerTypes data = new()
        {
            Available = json.GetNullableProperty("available").ToLongArray(),
            AvailableForMigration = json.GetNullableProperty("available_for_migration").ToLongArray(),
            Supported = json.GetNullableProperty("supported").ToLongArray(),
        };

        return data;
    }
}
