using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.Server;

internal class ServerResponse
{
    [JsonProperty("id")]
    public long? Id { get; set; } = null;
    [JsonProperty("name")]
    public string? Name { get; set; } = null;
}
