namespace Hetzner.Cloud.Models;

public enum ServerStatus
{
    Unknown,
    Running,
    Initializing,
    Starting,
    Stopping,
    Off,
    Deleting,
    Migrating,
    Rebuilding
}
