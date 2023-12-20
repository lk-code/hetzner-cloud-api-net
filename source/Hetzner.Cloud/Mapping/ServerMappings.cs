using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ServerMappings
{
    internal static Server ToServer(this JsonElement json)
    {
        Server data = new(json.GetProperty("id").GetInt64())
        {
            Name = json.GetProperty("name").GetString()!,
            Status = json.GetProperty("status").ToEnum<ServerStatus>(),
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
            IsoImage = json.GetProperty("iso").ToIsoImage(),
            RescueEnabled = json.GetProperty("rescue_enabled").GetBoolean(),
            LoadBalancers = json.GetNullableProperty("load_balancers").ToLongArray(),
            PrivateNetworks = json.GetProperty("private_net").ToPrivateNetworks(),
            Volumes = json.GetNullableProperty("volumes").ToLongArray(),
            Type = json.GetProperty("server_type").ToServerType(),
            PublicNetwork = json.GetProperty("public_net").ToPublicNetwork(),
        };

        return data;
    }
    
    internal static ServerType ToServerType(this JsonElement json)
    {
        ServerType data = new(json.GetProperty("id").GetInt64())
        {
            Cores = json.GetProperty("cores").GetInt64(),
            CpuType = json.GetProperty("cpu_type").ToEnum<ServerCpuTypes>(),
            Deprecated = json.GetProperty("deprecated").GetBoolean(),
            Description = json.GetProperty("description").GetString()!,
            Disk = json.GetProperty("disk").GetDouble(),
            Memory = json.GetProperty("memory").GetDouble(),
            Name = json.GetProperty("name").GetString()!,
            Prices = json.GetProperty("prices").ToPrices(),
            StorageType = json.GetProperty("storage_type").ToEnum<ServerStorageTypes>(),
        };

        return data;
    }
    
    internal static ServerProtection ToServerProtection(this JsonElement json)
    {
        ServerProtection data = new()
        {
            Delete = json.GetProperty("delete").GetBoolean(),
            Rebuild = json.GetProperty("rebuild").GetBoolean(),
        };

        return data;
    }
}
