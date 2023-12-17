namespace Hetzner.Cloud.Models;

public class Firewall(long id, FirewallStatus status)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Possible enum values: applied, pending
    /// Status of the Firewall on the Server
    /// </summary>
    public FirewallStatus Status { get; }
}