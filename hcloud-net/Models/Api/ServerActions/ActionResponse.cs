using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.ServerActions;

internal class ActionResponse
{
    [JsonProperty("command")]
    public string? Command { get; set; }
    [JsonProperty("error")]
    public ErrorResponse? Error { get; set; }
    [JsonProperty("finished")]
    public string? Finished { get; set; }
    [JsonProperty("id")]
    public long? Id { get; set; }
    [JsonProperty("progress")]
    public double? Progress { get; set; }
    [JsonProperty("resources")]
    public List<ResourceResponse>? Resources { get; set; }
    [JsonProperty("started")]
    public DateTime? Started { get; set; }
    [JsonProperty("status")]
    public string? Status { get; set; }
}
