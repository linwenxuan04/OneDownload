using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneDownload.Models.Json.Graph.Schemes;

public class FileScheme
{
    [JsonProperty("hashes")] public JObject Hashes { get; set; } = new();
    [JsonProperty("mimeType")] public string MimeType { get; set; } = "";
    [JsonProperty("processingMetadata")] public bool ProcessingMetadata { get; set; }
}