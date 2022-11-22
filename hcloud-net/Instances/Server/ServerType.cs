using lkcode.hetznercloudapi.Enums;

namespace lkcode.hetznercloudapi.Instances.Server;

public class ServerType
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; private set; } = 0;
    /// <summary>
    /// Unique identifier of the Server type
    /// </summary>
    public string Name { get; private set; } = string.Empty;
    /// <summary>
    /// Description of the Server type
    /// </summary>
    public string Description { get; private set; } = string.Empty;
    /// <summary>
    /// Number of cpu cores a Server of this type will have
    /// </summary>
    public int Cores { get; private set; } = 0;
    /// <summary>
    /// Memory a Server of this type will have in GB
    /// </summary>
    public int Memory { get; private set; } = 0;
    /// <summary>
    /// Disk size a Server of this type will have in GB
    /// </summary>
    public int Disk { get; private set; } = 0;
    /// <summary>
    /// True if Server type is deprecated
    /// </summary>
    public bool Deprecated { get; private set; } = false;
    /// <summary>
    /// Type of Server boot drive. Local has higher speed. Network has better availability.
    /// </summary>
    public ServerStorageType StorageType { get; private set; } = ServerStorageType.Local;
    /// <summary>
    /// Type of cpu
    /// </summary>
    public ServerCpuType CpuType { get; private set; } = ServerCpuType.Shared;
    /// <summary>
    /// Prices in different Locations
    /// </summary>
    public IEnumerable<ServerPrice> Prices { get; private set; } = new List<ServerPrice>();
}
