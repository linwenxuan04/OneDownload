using OneDownload.Models;
using Downloader;
using Newtonsoft.Json;

namespace OneDownload.Core;

public static class DownloadManager
{
    public static Dictionary<int, DownloadService> DownloadQueue = new();
    public static Dictionary<int, DownloadPackage> PauseQueue = new();

    public static List<DownloadService> DownloadTasks => DownloadQueue.Values.ToList();

    public static void CreateTask(DownloadEntity downloadEntity)
    {
        if (DownloadQueue.ContainsKey(downloadEntity.GetHashCode())) return;
        
        var service = new DownloadService(GetConfig());
        Console.WriteLine(downloadEntity.GetHashCode());
        DownloadQueue.Add(downloadEntity.GetHashCode(), service);

        Console.WriteLine(downloadEntity.Url);
        var task = service.DownloadFileTaskAsync(downloadEntity.Url, downloadEntity.Filepath);
        task.Start();
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
    
    public static void RemoveTask(DownloadEntity downloadEntity)
    {
        if (!DownloadQueue.ContainsKey(downloadEntity.GetHashCode())) return;
        
        var service = DownloadQueue[downloadEntity.GetHashCode()];
        service.CancelAsync();
        DownloadQueue.Remove(downloadEntity.GetHashCode());
    }
    
    public static void PauseAllTask()
    {
        foreach (var (hash, service) in DownloadQueue)
        {
            var package = service.Package;
            PauseQueue.Add(hash, package);
            service.CancelAsync();
        }
    }
    
    public static void ResumeAllTask()
    {
        foreach (var (hash, package) in PauseQueue)
        {
            var service = DownloadQueue[hash];
            service.DownloadFileTaskAsync(package);
        }
        PauseQueue.Clear();
    }
    
    public static void RemoveAllTask()
    {
        foreach (var (_, service) in DownloadQueue) service.CancelAsync();
        DownloadQueue.Clear();
    }

    private static DownloadConfiguration GetConfig()
    {
        if (Preferences.Get("DownloadConfig", null) is null)
        {
            var config = new DownloadConfiguration()
            {
                ParallelDownload = true,
                ChunkCount = 16,
                TempDirectory = Path.GetTempPath(),
                TempFilesExtension = ".onedownload",
                OnTheFlyDownload = false,
            };
            Preferences.Set("DownloadConfig", JsonConvert.SerializeObject(config));
            return config;
        }
        return JsonConvert.DeserializeObject<DownloadConfiguration>(Preferences.Get("DownloadConfig", null)!)!;
    }
    
    public static void SetConfig(DownloadConfiguration config)
    {
        Preferences.Set("DownloadConfig", JsonConvert.SerializeObject(config));
    }
}