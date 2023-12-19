namespace Hetzner.Cloud.Models;

public class PlacementGroup(long id)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Name of the Resource. Must be unique per Project.
    /// </summary>
    public string Name { get; internal set; }
    /// <summary>
    /// Point in time when the Resource was created (in ISO-8601 format)
    /// </summary>
    public DateTime Created { get; internal set; } = DateTime.MinValue;
    /// <summary>
    /// User-defined labels (key-value pairs)
    /// more informations: https://docs.hetzner.cloud/#labels
    /// </summary>
    public Dictionary<string, string> Labels { get; internal set; } = new();
    /// <summary>
    /// Possible enum values:
    /// spread
    /// Type of the Placement Group
    /// </summary>
    public string Type { get; internal set; }
    /// <summary>
    /// Array of IDs of Servers that are part of this Placement Group
    /// </summary>
    public long[] ServerIds { get; internal set; }
}