using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;

public class ReferenceScheme
{
    [JsonProperty("driveId")] public string DriveId { get; set; } = "";
    [JsonProperty("driveType")] public string DriveType { get; set; } = "";
    [JsonProperty("id")] public string Id { get; set; } = "";
    [JsonProperty("listId")] public string ListId { get; set; } = "";
    [JsonProperty("name")] public string Name { get; set; } = "";
    [JsonProperty("path")] public string Path { get; set; } = "";
    [JsonProperty("shareId")] public string ShareId { get; set; } = "";
    [JsonProperty("siteId")] public string SiteId { get; set; } = "";
}