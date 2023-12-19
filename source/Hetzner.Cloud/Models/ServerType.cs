namespace Hetzner.Cloud.Models;

public class ServerType(long id)
{
    /// <summary>
    /// ID of the Server type
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Number of cpu cores a Server of this type will have
    /// </summary>
    public long Cores { get; internal set; }
    /// <summary>
    /// Possible enum values: shared, dedicated
    /// Type of cpu
    /// </summary>
    public ServerCpuTypes CpuType { get; internal set; }
    /// <summary>
    /// True if Server type is deprecated
    /// </summary>
    public bool Deprecated { get; internal set; }
    /// <summary>
    /// Description of the Server type
    /// </summary>
    public string Description { get; internal set; } = string.Empty;
    /// <summary>
    /// Disk size a Server of this type will have in GB
    /// </summary>
    public double Disk { get; internal set; }
    /// <summary>
    /// Memory a Server of this type will have in GB
    /// </summary>
    public double Memory { get; internal set; }
    /// <summary>
    /// Unique identifier of the Server type
    /// </summary>
    public string Name { get; internal set; } = string.Empty;
    /// <summary>
    /// Prices in different Locations
    /// </summary>
    public Price[] Prices { get; internal set; } = Array.Empty<Price>();
    /// <summary>
    /// Possible enum values: local, network
    /// Type of Server boot drive. Local has higher speed. Network has better availability.
    /// </summary>
    public ServerStorageTypes StorageType { get; internal set; }
}