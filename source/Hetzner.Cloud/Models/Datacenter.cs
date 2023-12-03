namespace Hetzner.Cloud.Models;

public class Datacenter(long id)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; private set; } = id;
    /// <summary>
    /// Description of the Datacenter
    /// </summary>
    public string Description { get; internal set; } = string.Empty;
    /// <summary>
    /// The location of the datacenter
    /// </summary>
    public Location? Location { get; internal set; }
    /// <summary>
    /// Unique identifier of the Datacenter
    /// </summary>
    public string Name { get; internal set; } = string.Empty;
    /// <summary>
    /// The Server types the Datacenter can handle
    /// </summary>
    public ServerTypes? ServerTypes { get; internal set; }
}