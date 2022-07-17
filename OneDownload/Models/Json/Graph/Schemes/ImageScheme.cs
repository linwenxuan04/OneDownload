using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;

public class ImageScheme
{
    [JsonProperty("width")] public int Width { get; set; }
    [JsonProperty("height")] public int Height { get; set; }
}