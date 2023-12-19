namespace Hetzner.Cloud.Models;

public class ImageProtection
{
    /// <summary>
    /// If true, prevents the Resource from being deleted
    /// </summary>
    public bool Delete { get; internal set; }
}