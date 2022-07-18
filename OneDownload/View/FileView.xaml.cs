using OneDownload.Core;
using OneDownload.Models;
using OneDownload.Models.Json.Graph.Items;

namespace OneDownload.View;

public partial class FileView
{
    public FileView(List<DriveItems> itemsList)
    {
        InitializeComponent();
        
        FileList.ItemsSource = itemsList;
    }
    
    public void DownloadFile(object sender, EventArgs e)
    {
        Console.WriteLine("Downloading Items");
        var item = (sender as ImageButton)!.BindingContext as DriveItems;
        Console.WriteLine(item!.DownloadUrl);
        var entity = new DownloadEntity
        {
            Filename = item!.Name,
            Url = item.DownloadUrl,
            Filepath = $"/Users/wenxuanlin/Desktop/OneDownload/OneDownload/{item.Name}"
        };
        Console.WriteLine(Environment.CurrentDirectory);
        DownloadManager.CreateTask(entity);
    }
}