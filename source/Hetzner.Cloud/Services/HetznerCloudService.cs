using System.Net.Http.Headers;
using Hetzner.Cloud.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hetzner.Cloud.Services;

/// <inheritdoc/>
public class HetznerCloudService : IHetznerCloudService
{
    private readonly IConfiguration _configuration = null!;
    private HttpClient? _httpClient = null;

    public const string API_SERVER = "https://api.hetzner.cloud";

    private string? _apiToken = null;
    private readonly string _clientUserAgent;

    public HetznerCloudService(IConfiguration configuration)
    {
        this._configuration = configuration;

        // load config
        IConfigurationSection hcloudConfig = this._configuration.GetSection("HetznerCloud");

        this._clientUserAgent = hcloudConfig.GetSection("useragent").Value;

        this.LoadApiToken(hcloudConfig.GetSection("apitoken").Value);
    }

    /// <inheritdoc/>
    public void LoadApiToken(string apiToken)
    {
        this._apiToken = apiToken;

        if (this._httpClient is not null)
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);
        }
    }

    /// <inheritdoc/>
    public void ConfigureClient(IServiceProvider serviceProvider, HttpClient httpClient)
    {
        this._httpClient = httpClient;
        
        this._httpClient.BaseAddress = new Uri(API_SERVER);

        // httpClient.DefaultRequestHeaders.Add("User-Agent", this._clientUserAgent);
        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);
    }
}