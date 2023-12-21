using System.Net.Http.Headers;
using Hetzner.Cloud.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hetzner.Cloud.Services;

/// <inheritdoc/>
public class HetznerCloudService : IHetznerCloudService
{
    public Guid Id { get; } = Guid.NewGuid();
    private HttpClient? _httpClient;

    private const string API_SERVER = "https://api.hetzner.cloud";

    private string? _apiToken;
    private readonly string? _clientUserAgent;

    public HetznerCloudService(IConfiguration configuration)
    {
        // load config
        IConfigurationSection hcloudConfig = configuration.GetSection("HetznerCloud");

        string? userAgent = hcloudConfig.GetSection("useragent").Value;
        if (string.IsNullOrEmpty(userAgent))
            this._clientUserAgent = userAgent!;

        string? apiToken = hcloudConfig.GetSection("apitoken").Value;
        if (string.IsNullOrEmpty(apiToken))
            this.LoadApiToken(apiToken!);
    }

    /// <inheritdoc/>
    public void LoadApiToken(string apiToken)
    {
        this._apiToken = apiToken;

        if (this._httpClient is not null)
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);
            if (!string.IsNullOrEmpty(this._clientUserAgent))
            {
                this._httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(this._clientUserAgent));
            }
        }
    }

    /// <inheritdoc/>
    public void ConfigureClient(IServiceProvider serviceProvider, HttpClient httpClient)
    {
        this._httpClient = httpClient;

        this._httpClient.BaseAddress = new Uri(API_SERVER);

        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);
        if (!string.IsNullOrEmpty(this._clientUserAgent))
        {
            this._httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(this._clientUserAgent));
        }
    }
}