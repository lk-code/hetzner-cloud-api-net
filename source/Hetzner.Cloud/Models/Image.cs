namespace Hetzner.Cloud.Models;

public class Image(long id)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Type of cpu architecture this image is compatible with.
    /// </summary>
    public string Architecture { get; internal set; } = string.Empty;
    /// <summary>
    /// ID of Server the Image is bound to. Only set for Images of type backup.
    /// </summary>
    public long? BoundTo { get; internal set; }
    /// <summary>
    /// Point in time when the Resource was created (in ISO-8601 format)
    /// </summary>
    public DateTime Created { get; internal set; }
    /// <summary>
    /// Information about the Server the Image was created from
    /// </summary>
    public ImageCreatedFrom? CreatedFrom { get; internal set; }
    /// <summary>
    /// Point in time where the Image was deleted (in ISO-8601 format)
    /// </summary>
    public DateTime? Deleted { get; internal set; }
    /// <summary>
    /// Point in time when the Image is considered to be deprecated (in ISO-8601 format)
    /// </summary>
    public DateTime? Deprecated { get; internal set; }
    /// <summary>
    /// Description of the Image
    /// </summary>
    public string Description { get; internal set; } = string.Empty;
    /// <summary>
    /// Size of the disk contained in the Image in GB
    /// </summary>
    public double DiskSize { get; internal set; }
    /// <summary>
    /// Size of the Image file in our storage in GB. For snapshot Images this is the value relevant for calculating costs for the Image.
    /// </summary>
    public double? ImageSize { get; internal set; }
    /// <summary>
    /// User-defined labels (key-value pairs)
    /// </summary>
    public Dictionary<string, string> Labels { get; internal set; } = new();
    /// <summary>
    /// Unique identifier of the Image. This value is only set for system Images.
    /// </summary>
    public string? Name { get; internal set; }
    /// <summary>
    /// Flavor of operating system contained in the Image
    /// </summary>
    public OsFlavor OsFlavor { get; internal set; }= OsFlavor.Unknown;
    /// <summary>
    /// Operating system version
    /// </summary>
    public string? OsVersion { get; internal set; }
    /// <summary>
    /// Protection configuration for the Resource
    /// </summary>
    public ImageProtection? Protection { get; internal set; }
    /// <summary>
    /// Indicates that rapid deploy of the Image is available
    /// </summary>
    public bool RapidDeploy { get; internal set; }
    /// <summary>
    /// Whether the Image can be used or if it's still being created or unavailable
    /// </summary>
    public ImageStatus Status { get; internal set; } = ImageStatus.Unavailable;
    /// <summary>
    /// Type of the Image
    /// </summary>
    public ImageType Type { get; internal set; } = ImageType.System;
}