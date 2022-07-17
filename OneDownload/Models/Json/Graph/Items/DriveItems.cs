using Newtonsoft.Json;
using OneDownload.Models.Json.Graph.Schemes;

namespace OneDownload.Models.Json.Graph.Items;

public class DriveItems
{
    [JsonProperty("audio")] public AudioScheme? Audio { get; set; }
    [JsonProperty("cTag")] public string CTag { get; set; } = "";
    [JsonProperty("deleted")] public DeletedScheme? Deleted { get; set; }
    [JsonProperty("description")] public string Description { get; set; } = "";
    [JsonProperty("file")] public FileScheme? File { get; set; }
    [JsonProperty("fileSystemInfo")] public FileSystemScheme? FileSystemInfo { get; set; }
    [JsonProperty("folder")] public FolderScheme? Folder { get; set; }
    [JsonProperty("image")] public ImageScheme? Image { get; set; }
    [JsonProperty("size")] public long Size { get; set; }
    [JsonProperty("webDavUrl")] public string WebDavUrl { get; set; } = "";
    
    [JsonProperty("id")] public string Id { get; set; } = "";
    [JsonProperty("createdBy")] public IdentitySet CreatedBy { get; set; } = new();
    [JsonProperty("createdDateTime")] public string CreatedDateTime { get; set; } = "";
    [JsonProperty("eTag")] public string ETag { get; set; } = "";
    [JsonProperty("lastModifiedBy")] public IdentitySet LastModifiedBy { get; set; } = new();
    [JsonProperty("lastModifiedDateTime")] public string LastModifiedDateTime { get; set; } = "";
    [JsonProperty("name")] public string Name { get; set; } = "";
    [JsonProperty("parentReference")] public ReferenceScheme ParentReference { get; set; } = new();
    [JsonProperty("webUrl")] public string WebUrl { get; set; } = "";
    
    [JsonProperty("@microsoft.graph.conflictBehavior")] public string? ConflictBehavior { get; set; }
    [JsonProperty("@microsoft.graph.downloadUrl")] public string? DownloadUrl { get; set; }
    [JsonProperty("@microsoft.graph.sourceUrl")] public string? SourceUrl { get; set; }
}