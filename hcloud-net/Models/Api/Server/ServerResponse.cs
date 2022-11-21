using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.Server;

internal class ServerResponse
{
    [JsonProperty("id")]
    public long? Id { get; set; } = null;
    [JsonProperty("name")]
    public string? Name { get; set; } = null;
    [JsonProperty("status")]
    public string? Status { get; set; } = null;
    [JsonProperty("created")]
    public DateTime? Created { get; set; } = null;
    [JsonProperty("included_traffic")]
    public long? IncludedTraffic { get; set; } = null;
    [JsonProperty("ingoing_traffic")]
    public long? IngoingTraffic { get; set; } = null;
    [JsonProperty("outgoing_traffic")]
    public long? OutgoingTraffic { get; set; } = null;
    [JsonProperty("labels")]
    public Dictionary<string, string>? Labels { get; set; } = null;
}
