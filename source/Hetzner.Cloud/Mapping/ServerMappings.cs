using System.Text.Json;
using Hetzner.Cloud.Enums;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ServerMappings
{
    internal static Server? ToServer(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        Server data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
            Status = json.GetProperty("status").ToServerStatus(),
            Created = json.GetProperty("created").GetDateTime(),
            IncludedTraffic = json.GetNullableProperty("included_traffic").GetInt64(),
            IngoingTraffic = json.GetNullableProperty("ingoing_traffic").GetInt64(),
            OutgoingTraffic = json.GetNullableProperty("outgoing_traffic").GetInt64(),
            Locked = json.GetProperty("locked").GetBoolean(),
            Labels = json.GetProperty("labels").ToDictionary(),
            BackupWindow = json.GetProperty("backup_window").GetString(),
            PrimaryDiskSize = json.GetProperty("primary_disk_size").GetInt64(),
            PlacementGroup = json.GetProperty("placement_group").ToPlacementGroup(),
            Datacenter = json.GetProperty("datacenter").ToDatacenter(),
            Protection = json.GetProperty("protection").ToServerProtection(),
            Image = json.GetProperty("image").ToImage(),
        };

        return data;
    }
    
    internal static ServerStatus ToServerStatus(this JsonElement json)
    {
        string? value = json.GetString();
        ServerStatus enumValue = ServerStatus.Unknown;
        if (!string.IsNullOrEmpty(value))
        {
            string parsedEnumValue = value;
            parsedEnumValue = parsedEnumValue.First().ToString().ToUpper() + parsedEnumValue.Substring(1);
            Enum.TryParse(parsedEnumValue, out enumValue);
        }

        return enumValue;
    }
    
    internal static ServerProtection? ToServerProtection(this JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        ServerProtection data = new()
        {
            Delete = json.GetProperty("delete").GetBoolean()!,
            Rebuild = json.GetProperty("rebuild").GetBoolean()!,
        };

        return data;
    }
}
