using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api;

internal class MetaResponse
{
    [JsonProperty("pagination")]
    public PaginationResponse? Pagination { get; set; } = null;
}
