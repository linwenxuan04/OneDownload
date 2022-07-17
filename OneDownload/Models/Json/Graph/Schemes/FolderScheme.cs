using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;

public class FolderScheme
{
    [JsonProperty("childCount")] public int ChildCount { get; set; }
}