using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api;

internal class PaginationResponse
{
    [JsonProperty("last_page")]
    public int? LastPage { get; set; } = null;
    [JsonProperty("next_page")]
    public int? NextPage { get; set; } = null;
    [JsonProperty("page")]
    public int? Page { get; set; } = null;
    [JsonProperty("per_page")]
    public int? ItemsPerPage { get; set; } = null;
    [JsonProperty("previous_page")]
    public int? PreviousPage { get; set; } = null;
    [JsonProperty("total_entries")]
    public int? TotalEntries { get; set; } = null;
}
