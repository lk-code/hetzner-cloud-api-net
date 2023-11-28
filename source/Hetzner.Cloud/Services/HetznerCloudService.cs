using System.Net.Http.Headers;
using Hetzner.Cloud.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hetzner.Cloud.Services;

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
        IConfigurationSection hcloudConfig = this._configuration.GetSection("HetznerCloud");

        this._clientUserAgent = hcloudConfig.GetSection("useragent").Value;

        this.LoadToken(hcloudConfig.GetSection("apitoken").Value);
    }

    /// <inheritdoc/>
    public void LoadToken(string apiToken)
    {
        this._apiToken = apiToken;
    }

    /// <inheritdoc/>
    public void ConfigureClient(IServiceProvider serviceProvider, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(API_SERVER);

        // httpClient.DefaultRequestHeaders.Add("User-Agent", this._clientUserAgent);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);
    }
}