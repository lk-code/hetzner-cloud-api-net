using lkcode.hetznercloudapi.Enums;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances;
using lkcode.hetznercloudapi.Models.Api.Server;

namespace lkcode.hetznercloudapi.Mapping;

internal static class ServerMappings
{
    public static Server ToServerInstance(this ServerResponse serverResponse)
    {
        ServerStatus serverStatus = ServerStatus.Unknown;
        Enum.TryParse(serverResponse.Status, out serverStatus);

        Server server = new Server(serverResponse.Id.EnsureWithException("the server-id can't be null (invalid api response)"))
        {
            Name = serverResponse.Name.Ensure(),
            Status = serverStatus,
            Created = serverResponse.Created.EnsureWithException("the created property can't be null (invalid api response)")
        };

        return server;
    }
}
