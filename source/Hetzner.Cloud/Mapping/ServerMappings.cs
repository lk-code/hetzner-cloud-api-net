using System.Text.Json;
using Hetzner.Cloud.Enums;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ServerMappings
{
    internal static Server ToServer(this JsonElement json)
    {
        string? serverStatusValue = json.GetProperty("status").GetString();
        ServerStatus serverStatus = ServerStatus.Unknown;
        if (!string.IsNullOrEmpty(serverStatusValue))
        {
            string status = serverStatusValue;
            status = status.First().ToString().ToUpper() + status.Substring(1);
            Enum.TryParse(status, out serverStatus);
        }

        Server data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
            Status = serverStatus,
            Created = DateTime.Parse(json.GetProperty("created").GetString()!),
            IncludedTraffic = json.GetProperty("included_traffic").GetInt64(),
            IngoingTraffic = json.GetProperty("ingoing_traffic").GetInt64(),
            OutgoingTraffic = json.GetProperty("outgoing_traffic").GetInt64(),
            Locked = json.GetProperty("locked").GetBoolean(),
            Labels = json.GetProperty("labels").ToDictionary(),
            BackupWindow = json.GetProperty("backup_window").GetString(),
            PrimaryDiskSize = json.GetProperty("primary_disk_size").GetInt64(),
            PlacementGroup = json.GetProperty("placement_group").ToPlacementGroup(),
            Datacenter = json.GetProperty("datacenter").ToDatacenter(),
        };

        return data;
    }
}
