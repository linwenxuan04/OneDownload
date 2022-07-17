using System.Net.Http.Headers;
using Newtonsoft.Json;
using OneDownload.Models.Json.Graph;

namespace OneDownload.Core;

public class DriverManager
{
    private readonly HttpClient _client;
    private const string BaseUrl = "https://graph.microsoft.com/v1.0";
    
    public DriverManager()
    {
        _client = new HttpClient();
        
        var token = AuthManager.GetToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token?.AccessToken);
    }
    
    public DriverManager(string accessToken)
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
    }
    
    public async Task<Folder?> GetDriveRoot() => await GetAsync<Folder>($"{BaseUrl}/me/drive/root/children");

    private async Task<T?> GetAsync<T>(string url)
    {
        var response = await _client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(json);
    }
}