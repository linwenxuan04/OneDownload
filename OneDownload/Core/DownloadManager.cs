using OneDownload.Models;
using Downloader;
using Newtonsoft.Json;

namespace OneDownload.Core;

public class DownloadManager
{
    public static Dictionary<int, DownloadService> DownloadQueue;
    public static Dictionary<int, DownloadPackage> PauseQueue;

    public static List<DownloadService> DownloadTasks => DownloadQueue.Values.ToList();

    static DownloadManager()
    {
        DownloadQueue = new Dictionary<int, DownloadService>();
    }
    
    public static void CreateTask(DownloadEntity downloadEntity)
    {
        if (DownloadQueue.ContainsKey(downloadEntity.GetHashCode())) return;
        
        var service = new DownloadService();
        DownloadQueue.Add(downloadEntity.GetHashCode(), service);
        
        service.DownloadFileTaskAsync(downloadEntity.Url, downloadEntity.Filepath);
    }
    
    public static void PauseTask(DownloadEntity downloadEntity)
    {
        if (!DownloadQueue.ContainsKey(downloadEntity.GetHashCode())) return;
        
        var service = DownloadQueue[downloadEntity.GetHashCode()];
        var package = service.Package;
        
        PauseQueue.Add(downloadEntity.GetHashCode(), package);
        service.CancelAsync();
    }
    
    public static void ResumeTask(DownloadEntity downloadEntity)
    {
        if (!PauseQueue.ContainsKey(downloadEntity.GetHashCode())) return;
        
        var package = PauseQueue[downloadEntity.GetHashCode()];
        var service = DownloadQueue[downloadEntity.GetHashCode()];
        service.DownloadFileTaskAsync(package);
        
        PauseQueue.Remove(downloadEntity.GetHashCode());
    }

    private static DownloadConfiguration GetConfig()
    {
        if (Preferences.Get("DownloadConfig", null) is null)
        {
            var config = new DownloadConfiguration()
            {
                ParallelDownload = true,
                ChunkCount = 16
            };
            Preferences.Set("DownloadConfig", JsonConvert.SerializeObject(config));
            return config;
        }
        return JsonConvert.DeserializeObject<DownloadConfiguration>(Preferences.Get("DownloadConfig", null)!)!;
    }
}