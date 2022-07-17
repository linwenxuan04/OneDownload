using System.Net;
using System.Text;
using Newtonsoft.Json;
using Authorization = OneDownload.Models.Json.Graph.Authorization;

namespace OneDownload.Core;

public static class AuthManager
{
    private static HttpClient _httpClient;

    static AuthManager() => _httpClient = new HttpClient();

    public static async Task<string> GetOAuthUrl()
    {
        var scopes = Constants.Scopes.Aggregate("", (current, scope) => current + scope + " ");
        var parameters = new Dictionary<string, string>
        {
            { "client_id", Constants.ClientId },
            { "response_type", "code" },
            { "redirect_uri", Constants.RedirectUri },
            { "scope", scopes },
        };
        var queryString = await SubmitDictionaryToForm(parameters);
        return $"https://login.microsoftonline.com/common/oauth2/v2.0/authorize?{queryString}";
    }

    public static async Task<Authorization> FetchAccessToken(string code)
    {
        const string url = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
        
        var parameters = new Dictionary<string, string>
        {
            { "client_id", Constants.ClientId },
            { "redirect_uri", Constants.RedirectUri },
            { "code", code },
            { "grant_type", "authorization_code" },
        };
        var queryString = await SubmitDictionaryToForm(parameters);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded")
        };
        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        return JsonConvert.DeserializeObject<Authorization>(responseString) ?? throw new Exception();
    }
    
    public static async Task<Authorization> RefreshAccessToken(string refreshToken)
    {
        const string url = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        var parameters = new Dictionary<string, string>
        {
            { "client_id", Constants.ClientId },
            { "redirect_uri", Constants.RedirectUri },
            { "refresh_token", refreshToken },
            { "grant_type", "refresh_token" },
        };
        var queryString = await SubmitDictionaryToForm(parameters);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded")
        };
        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        return JsonConvert.DeserializeObject<Authorization>(responseString) ?? throw new Exception();
    }

    public static async Task<string?> AuthListener()
    {
        using var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:11451/");
        listener.Start();
        
        var context = await listener.GetContextAsync();
        var code = context.Request.QueryString["code"];
        listener.Stop();
        return code ?? null;
    }

    public static void SaveToken(Authorization authorization) => 
        Preferences.Set("UserToken", JsonConvert.SerializeObject(authorization));
    
    public static Authorization? GetToken() => 
        JsonConvert.DeserializeObject<Authorization>(Preferences.Get("UserToken", ""));

    private static async Task<string> SubmitDictionaryToForm(Dictionary<string, string> dictionary) =>
        await new FormUrlEncodedContent(dictionary).ReadAsStringAsync();
}