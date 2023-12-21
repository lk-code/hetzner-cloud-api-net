namespace Hetzner.Cloud.Models;

public class ServerActionResource(long id, string type)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;

    /// <summary>
    /// Type of resource referenced
    /// </summary>
    public string Type { get; } = type;
}