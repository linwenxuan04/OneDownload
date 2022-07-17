using Newtonsoft.Json;
using OneDownload.Models.Json.Graph.Items;

namespace OneDownload.Models.Json.Graph;

public class Folder
{
    [JsonProperty("@odata.context")] public string OdataContext { get; set; } = "";
    [JsonProperty("value")] public List<DriveItems> Value { get; set; } = new();
}