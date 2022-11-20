using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Instances;
using lkcode.hetznercloudapi.Models.Api.Server;

namespace lkcode.hetznercloudapi.Mapping;

internal static class ServerMappings
{
    public static Server ToServerInstance(this ServerResponse serverResponse)
    {
        Server server = new Server(serverResponse.Id.EnsureWithException("the server-id can't be null (invalid api response)"))
        {
            Name = serverResponse.Name.Ensure()
        };

        return server;
    }
}
