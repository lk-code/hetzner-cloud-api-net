namespace Hetzner.Cloud.Models;

public class ServerActionResource(long id, string type)
{
    /// <summary>
    /// ID of the Action. Limited to 52 bits to ensure compatability with JSON double precision floats.
    /// </summary>
    public long Id { get; } = id;

    /// <summary>
    /// Command executed in the Action
    /// </summary>
    public string Type { get; } = type;
}