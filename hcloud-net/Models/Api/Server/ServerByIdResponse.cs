using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.Server;

internal class ServerByIdResponse
{
    [JsonProperty("server")]
    public ServerResponse? Server { get; set; } = null;
}
