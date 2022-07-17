using OneDownload.Core;

namespace OneDownload.View;

public partial class Authorization
{
    public Authorization()
    {
        InitializeComponent();

        if (AuthManager.GetToken() is null) return;
        AuthImage.Source = ImageSource.FromFile("checkshield.png");
        UrlText.Text = "Token saved. click button to proceed";
        AuthBtn.Text = "Proceed";
    }
    
    private async void OnAuthorization(object sender, EventArgs e)
    {
        if (AuthManager.GetToken() is null)
        {
            var url = await AuthManager.GetOAuthUrl();
            Console.WriteLine(url);
            UrlText.Text = $"URL: {url}";
            Indicator.IsRunning = true;
            AuthImage.IsVisible = false;
            
            var code = await AuthManager.AuthListener();
            var accessToken = await AuthManager.FetchAccessToken(code!);

            AuthManager.SaveToken(accessToken);
            UrlText.Text = "Authorization completed";
            Indicator.IsRunning = false;
            AuthImage.Source = ImageSource.FromFile("checkshield.png");
            AuthImage.IsVisible = true;
            AuthBtn.IsVisible = false;
            FileViewBtn.IsVisible = true;
        }
        else
        {
            OnFileView(sender, e);
        }
    }
    
    private async void OnFileView(object sender, EventArgs e)
    {
        var token = AuthManager.GetToken();
        var refreshedToken = await AuthManager.RefreshAccessToken(token!.RefreshToken);
        AuthManager.SaveToken(refreshedToken);

        var root = await new DriverManager().GetDriveRoot() 
                   ?? throw new Exception("Failed to get drive root");

        await Navigation.PopAsync();
        await Navigation.PushAsync(new FileView(root.Value));
    }
}