namespace Hetzner.Cloud.Models;

public class ImageCreatedFrom(long id)
{
    /// <summary>
    /// ID of the Server the Image was created from
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Server name at the time the Image was created
    /// </summary>
    public string Name { get; internal set; }
}