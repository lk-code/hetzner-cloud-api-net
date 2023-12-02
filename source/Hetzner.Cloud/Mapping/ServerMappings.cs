﻿using System.Text.Json;
using Hetzner.Cloud.Enums;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ServerMappings
{
    internal static Server ToServer(this JsonElement serverJson)
    {
        string? serverStatusValue = serverJson.GetProperty("status").GetString();
        ServerStatus serverStatus = ServerStatus.Unknown;
        if (!string.IsNullOrEmpty(serverStatusValue))
        {
            string status = serverStatusValue;
            status = status.First().ToString().ToUpper() + status.Substring(1);
            Enum.TryParse(status, out serverStatus);
        }

        Server server = new(serverJson.GetProperty("id").GetInt64())
        {
            Name = serverJson.GetProperty("name").GetString()!,
            Status = serverStatus,
            Created = DateTime.Parse(serverJson.GetProperty("created").GetString()!),
            IncludedTraffic = serverJson.GetProperty("included_traffic").GetInt64(),
            IngoingTraffic = serverJson.GetProperty("ingoing_traffic").GetInt64(),
            OutgoingTraffic = serverJson.GetProperty("outgoing_traffic").GetInt64(),
            Locked = serverJson.GetProperty("locked").GetBoolean(),
            Labels = serverJson.GetProperty("labels").ToDictionary(),
            BackupWindow = serverJson.GetProperty("backup_window").GetString(),
            PrimaryDiskSize = serverJson.GetProperty("primary_disk_size").GetInt64(),
            PlacementGroup = serverJson.GetProperty("placement_group").ToPlacementGroup(),
        };

        return server;
    }
}
