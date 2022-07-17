using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;

public class DeletedScheme
{
    [JsonProperty("state")] public string State { get; set; } = "";
}