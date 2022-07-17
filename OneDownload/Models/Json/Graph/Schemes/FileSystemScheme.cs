using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;

public class FileSystemScheme
{
    [JsonProperty("createdDateTime")] public string CreatedDateTime { get; set; } = "";
    [JsonProperty("lastAccessedDateTime")] public string LastAccessedDateTime { get; set; } = "";
    [JsonProperty("lastModifiedDateTime")] public string LastModifiedDateTime { get; set; } = "";
}