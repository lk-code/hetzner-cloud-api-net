namespace Hetzner.Cloud.Models;

public class IsoImage(long id)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Possible enum values: x86, arm
    /// Type of cpu architecture this iso is compatible with. Null indicates no restriction on the architecture (wildcard).
    /// </summary>
    public IsoImageArchitecture? Architecture { get; internal set; }
    /// <summary>
    /// ISO 8601 timestamp of deprecation, null if ISO is still available. After the deprecation time it will no longer be possible to attach the ISO to Servers.
    /// </summary>
    public DateTime? Deprecated { get; internal set; }
    /// <summary>
    /// Describes if, when & how the resources was deprecated. If this field is set to null the resource is not deprecated. If it has a value, it is considered deprecated.
    /// </summary>
    public IsoImageDeprecation? Deprecation { get; internal set; }
    /// <summary>
    /// Description of the ISO
    /// </summary>
    public string Description { get; internal set; } = string.Empty;
    /// <summary>
    /// Unique identifier of the ISO. Only set for public ISOs
    /// </summary>
    public string? Name { get; internal set; }
    /// <summary>
    /// Possible enum values: public, private
    /// Type of the ISO
    /// </summary>
    public IsoImageType? Type { get; internal set; }
}