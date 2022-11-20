using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.Server;

internal class ServerAllResponse
{
    [JsonProperty("meta")]
    public MetaResponse? Meta { get; set; } = null;
    [JsonProperty("servers")]
    public IEnumerable<ServerResponse>? Servers { get; set; } = null!;
}
