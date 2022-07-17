using OneDownload.Models.Json.Graph.Items;

namespace OneDownload.View;

public partial class FileView
{
    public FileView(List<DriveItems> itemsList)
    {
        InitializeComponent();
        
        FileList.ItemsSource = itemsList;
    }
}