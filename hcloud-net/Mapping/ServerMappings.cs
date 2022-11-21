using lkcode.hetznercloudapi.Enums;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances;
using lkcode.hetznercloudapi.Models.Api.Server;

namespace lkcode.hetznercloudapi.Mapping;

internal static class ServerMappings
{
    internal static Server ToServerInstance(this ServerResponse serverResponse)
    {
        ServerStatus serverStatus = ServerStatus.Unknown;
        Enum.TryParse(serverResponse.Status, out serverStatus);

        Server server = new Server(serverResponse.Id.EnsureWithException("the server-id can't be null (invalid api response)"))
        {
            Name = serverResponse.Name.Ensure(),
            Status = serverStatus,
            Created = serverResponse.Created.EnsureWithException("the created property can't be null (invalid api response)"),
            IncludedTraffic = serverResponse.IncludedTraffic.EnsureWithException("the included-traffic property can't be null (invalid api response)"),
            IngoingTraffic = serverResponse.IngoingTraffic.EnsureWithException("the ingoing-traffic property can't be null (invalid api response)"),
            OutgoingTraffic = serverResponse.OutgoingTraffic.EnsureWithException("the outgoing-traffic property can't be null (invalid api response)"),
        };

        return server;
    }
}
