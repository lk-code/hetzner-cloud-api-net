using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.Server;

internal class GetAllResponse
{
    [JsonProperty("meta")]
    public MetaResponse? Meta { get; set; }
    [JsonProperty("servers")]
    public IEnumerable<ServerResponse>? Servers { get; set; }
}
