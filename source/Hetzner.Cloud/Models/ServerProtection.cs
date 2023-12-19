namespace Hetzner.Cloud.Models;

public class ServerProtection
{
    /// <summary>
    /// If true, prevents the Server from being deleted
    /// </summary>
    public bool Delete { get; internal set; }
    /// <summary>
    /// If true, prevents the Server from being rebuilt
    /// </summary>
    public bool Rebuild { get; internal set; }
}