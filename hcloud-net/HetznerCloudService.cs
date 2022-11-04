using Microsoft.Extensions.Configuration;

namespace lkcode.hetznercloudapi;

public class HetznerCloudService : IHetznerCloudService
{
    private readonly IConfiguration _configuration;

    private readonly string _apiToken;

    public HetznerCloudService(IConfiguration configuration)
    {
        this._configuration = configuration;

        // load config
        IConfigurationSection hcloudConfig = this._configuration.GetSection("hcloud");
        this._apiToken = hcloudConfig.GetSection("apitoken").Value;
    }
}
