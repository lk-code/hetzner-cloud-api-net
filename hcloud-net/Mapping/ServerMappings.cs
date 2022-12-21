using lkcode.hetznercloudapi.Enums;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances.Server;
using lkcode.hetznercloudapi.Models.Api.Server;

namespace lkcode.hetznercloudapi.Mapping;

internal static class ServerMappings
{
    internal static Server ToServer(this ServerResponse serverResponse)
    {
        ServerStatus serverStatus = ServerStatus.Unknown;
        Enum.TryParse(serverResponse.Status, out serverStatus);

        Server server = new Server(serverResponse.Id.Ensure("the server-id can't be null (invalid api response)"))
        {
            Name = serverResponse.Name.Ensure("the name property can't be null (invalid api response)"),
            Status = serverStatus,
            Created = serverResponse.Created.Ensure("the created property can't be null (invalid api response)"),
            IncludedTraffic = serverResponse.IncludedTraffic.Ensure("the included-traffic property can't be null (invalid api response)"),
            IngoingTraffic = serverResponse.IngoingTraffic.Ensure("the ingoing-traffic property can't be null (invalid api response)"),
            OutgoingTraffic = serverResponse.OutgoingTraffic.Ensure("the outgoing-traffic property can't be null (invalid api response)"),
            Labels = serverResponse.Labels.Ensure()
        };

        return server;
    }
}
