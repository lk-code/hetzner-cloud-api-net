using lkcode.hetznercloudapi.Exceptions;
using lkcode.hetznercloudapi.Helper;
using lkcode.hetznercloudapi.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace lkcode.hetznercloudapi.Services;

/// <inheritdoc/>
public class HetznerCloudService : IHetznerCloudService
{
    private readonly IConfiguration _configuration = null!;

    public const string API_SERVER = "https://api.hetzner.cloud/v1";

    private string? _apiToken = null;
    private readonly string _clientUserAgent;

    public HetznerCloudService(IConfiguration configuration)
    {
        this._configuration = configuration;

        // load config
        IConfigurationSection hcloudConfig = this._configuration.GetSection("hcloud");

        this._clientUserAgent = hcloudConfig.GetSection("useragent").Value;

        this.LoadToken(hcloudConfig.GetSection("apitoken").Value);
    }

    /// <inheritdoc/>
    public void LoadToken(string apiToken)
    {
        this._apiToken = apiToken;

        if (string.IsNullOrEmpty(this._apiToken))
        {
            // no api token was given
        }
    }

    /// <summary>
    /// Verifies the API token.
    /// </summary>
    private void VerifyApiToken()
    {
        if (string.IsNullOrEmpty(this._apiToken) ||
            string.IsNullOrWhiteSpace(this._apiToken))
        {
            throw new InvalidAccessTokenException("No api token has been set.");
        }
    }

    /// <summary>
    /// Returns the HttpClient for the hetzner cloud api
    /// </summary>
    /// <returns></returns>
    private HttpClient GetHttpClient()
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", this._clientUserAgent);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);

        return client;
    }

    /// <inheritdoc/>
    public async Task<string> PostRequest(string action,
        Dictionary<string, object>? arguments = null)
    {
        this.VerifyApiToken();

        StringContent? argumentsContent = null;

        if (arguments != null &&
            arguments.Count > 0)
        {
            string argumentsJsonContent = JsonConvert.SerializeObject(arguments);
            argumentsContent = new StringContent(argumentsJsonContent,
                Encoding.UTF8,
                "application/json");
        }

        HttpClient client = this.GetHttpClient();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, API_SERVER + action);
        if (argumentsContent != null)
        {
            request.Content = argumentsContent;
        }

        HttpResponseMessage httpResponse = await client.SendAsync(request);
        string response = await httpResponse.Content.ReadAsStringAsync();

        return response;
    }

    /// <inheritdoc/>
    public async Task<T?> PostRequest<T>(string action,
        Dictionary<string, object>? arguments = null)
    {
        string response = await this.PostRequest(action, arguments);

        return JsonConvert.DeserializeObject<T>(response);
    }

    /// <inheritdoc/>
    public async Task<string> GetRequest(string action, Dictionary<string, string>? arguments = null)
    {
        this.VerifyApiToken();
        HttpClient client = this.GetHttpClient();

        string requestUrl = $"{API_SERVER}{action}{arguments.ToQueryString()}";
        string response = await client.GetStringAsync(requestUrl);

        return response;
    }

    /// <inheritdoc/>
    public async Task<T?> GetRequest<T>(string action, Dictionary<string, string>? arguments = null)
    {
        string response = await this.GetRequest(action, arguments);

        T result = JsonConvert.DeserializeObject<T>(response);

        return result;
    }
}
