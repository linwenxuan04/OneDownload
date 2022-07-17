using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;


public class IdentitySet
{
    [JsonProperty("application")] public IdentityScheme? Application { get; set; }
    [JsonProperty("device")] public IdentityScheme? Device { get; set; }
    [JsonProperty("group")] public IdentityScheme? Group { get; set; }
    [JsonProperty("user")] public IdentityScheme? User { get; set; }
}

public class IdentityScheme
{
    [JsonProperty("displayName")] public string DisplayName { get; set; } = "";
    [JsonProperty("id")] public string Id { get; set; } = "";
}