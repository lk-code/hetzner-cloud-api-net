using Newtonsoft.Json;

namespace lkcode.hetznercloudapi.Models.Api.ServerActions;

internal class ShutdownResponse
{
    [JsonProperty("action")]
    public ActionResponse? Action { get; set; }
}
