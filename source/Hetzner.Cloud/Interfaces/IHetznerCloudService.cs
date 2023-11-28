namespace Hetzner.Cloud.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IHetznerCloudService
{
    /// <summary>
    /// load a new api-token for the hetzner cloud
    /// </summary>
    /// <param name="apiToken">the api-token</param>
    void LoadToken(string apiToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="httpClient"></param>
    void ConfigureClient(IServiceProvider serviceProvider, HttpClient httpClient);
}
