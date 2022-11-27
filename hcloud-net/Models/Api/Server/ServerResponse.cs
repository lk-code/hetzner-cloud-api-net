using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.Server;

internal class ServerResponse
{
    [JsonProperty("id")]
    public long? Id { get; set; }
    [JsonProperty("name")]
    public string? Name { get; set; }
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("created")]
    public DateTime? Created { get; set; }
    [JsonProperty("included_traffic")]
    public long? IncludedTraffic { get; set; }
    [JsonProperty("ingoing_traffic")]
    public long? IngoingTraffic { get; set; }
    [JsonProperty("outgoing_traffic")]
    public long? OutgoingTraffic { get; set; }
    [JsonProperty("labels")]
    public Dictionary<string, string>? Labels { get; set; }
}
