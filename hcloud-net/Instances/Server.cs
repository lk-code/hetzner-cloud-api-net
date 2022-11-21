using lkcode.hetznercloudapi.Enums;

namespace lkcode.hetznercloudapi.Instances;

public class Server
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; private set; } = 0;
    /// <summary>
    /// Name of the Server (must be unique per Project and a valid hostname as per RFC 1123)
    /// </summary>
    public string Name { get; internal set; } = string.Empty;
    /// <summary>
    /// Status of the Server
    /// </summary>
    public ServerStatus Status { get; internal set; } = ServerStatus.Unknown;
    /// <summary>
    /// Point in time when the Resource was created
    /// </summary>
    public DateTime Created { get; internal set; } = DateTime.MinValue;
    /// <summary>
    /// Inbound Traffic for the current billing period in bytes
    /// </summary>
    public long IncludedTraffic { get; internal set; } = 0;
    /// <summary>
    /// Inbound Traffic for the current billing period in bytes
    /// </summary>
    public long IngoingTraffic { get; internal set; } = 0;
    /// <summary>
    /// Outbound Traffic for the current billing period in bytes
    /// </summary>
    public long OutgoingTraffic { get; internal set; } = 0;

    public Server(long id)
    {
        Id = id;
    }
}
