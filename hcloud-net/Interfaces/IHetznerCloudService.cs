namespace lkcode.hetznercloudapi.Interfaces;

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
    /// Sends a POST request to the cloud API.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    Task<string> PostRequest(string action, Dictionary<string, object>? arguments = null);

    /// <summary>
    /// Sends a POST request to the cloud API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    Task<T?> PostRequest<T>(string action, Dictionary<string, object>? arguments = null);

    /// <summary>
    /// Sends a GET request to the cloud API.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    Task<string> GetRequest(string action, Dictionary<string, string>? arguments = null);

    /// <summary>
    /// Sends a GET request to the cloud API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    Task<T?> GetRequest<T>(string action, Dictionary<string, string>? arguments = null);
}
