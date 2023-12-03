namespace Hetzner.Cloud.Models;

public class ServerTypes
{
    /// <summary>
    /// IDs of Server types that are supported and for which the Datacenter has enough resources left
    /// </summary>
    public long[] Available { get; internal set; }
    /// <summary>
    /// IDs of Server types that are supported and for which the Datacenter has enough resources left
    /// </summary>
    public long[] AvailableForMigration { get; internal set; }
    /// <summary>
    /// IDs of Server types that are supported in the Datacenter
    /// </summary>
    public long[] Supported { get; internal set; }
}