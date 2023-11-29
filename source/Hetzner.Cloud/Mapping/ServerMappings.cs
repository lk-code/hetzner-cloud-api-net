using System.Text.Json;
using Hetzner.Cloud.Instances.Server;
using lkcode.hetznercloudapi.Enums;

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
            // Name = serverResponse.Name.Ensure("the name property can't be null (invalid api response)"),
            Status = serverStatus,
            // Created = serverResponse.Created.Ensure("the created property can't be null (invalid api response)"),
            // IncludedTraffic = serverResponse.IncludedTraffic.Ensure("the included-traffic property can't be null (invalid api response)"),
            // IngoingTraffic = serverResponse.IngoingTraffic.Ensure("the ingoing-traffic property can't be null (invalid api response)"),
            // OutgoingTraffic = serverResponse.OutgoingTraffic.Ensure("the outgoing-traffic property can't be null (invalid api response)"),
            // Labels = serverResponse.Labels.Ensure()
        };

        return server;
    }
}
