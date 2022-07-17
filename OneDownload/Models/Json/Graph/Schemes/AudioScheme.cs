using Newtonsoft.Json;

namespace OneDownload.Models.Json.Graph.Schemes;

public class AudioScheme
{
    [JsonProperty("album")] public string Album { get; set; } = "";
    [JsonProperty("albumArtist")] public string AlbumArtist { get; set; } = "";
    [JsonProperty("artist")] public string Artist { get; set; } = "";
    [JsonProperty("bitrate")] public int Bitrate { get; set; }
    [JsonProperty("composers")] public string Composers { get; set; } = "";
    [JsonProperty("copyright")] public string Copyright { get; set; } = "";
    [JsonProperty("disc")] public int Disc { get; set; }
    [JsonProperty("discCount")] public int DiscCount { get; set; }
    [JsonProperty("duration")] public int Duration { get; set; }
    [JsonProperty("genre")] public string Genre { get; set; } = "";
    [JsonProperty("hasDrm")] public bool HasDrm { get; set; }
    [JsonProperty("isVariableBitrate")] public bool IsVariableBitrate { get; set; }
    [JsonProperty("title")] public string Title { get; set; } = "";
    [JsonProperty("track")] public int Track { get; set; }
    [JsonProperty("trackCount")] public int TrackCount { get; set; }
    [JsonProperty("year")] public int Year { get; set; }
}