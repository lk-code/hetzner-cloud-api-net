using lkcode.hetznercloudapi.Models.Api.Server;
using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.ServerActions;

internal class GetAllResponse
{
    [JsonProperty("meta")]
    public MetaResponse? Meta { get; set; }
    [JsonProperty("actions")]
    public IEnumerable<ActionResponse>? Actions { get; set; }
}
