using lkcode.hetznercloudapi.Interfaces;
using Microsoft.Extensions.Configuration;

namespace lkcode.hetznercloudapi.Services;

/// <inheritdoc/>
public class HetznerCloudService : IHetznerCloudService
{
    private readonly IConfiguration _configuration;

    private string? _apiToken = null;

    public HetznerCloudService(IConfiguration configuration)
    {
        this._configuration = configuration;

        // load config
        IConfigurationSection hcloudConfig = this._configuration.GetSection("hcloud");
        string apiToken = hcloudConfig.GetSection("apitoken").Value;

        this.LoadToken(apiToken);
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
}
