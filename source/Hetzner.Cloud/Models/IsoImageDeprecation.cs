namespace Hetzner.Cloud.Models;

public class IsoImageDeprecation
{
    /// <summary>
    /// Date of when the deprecation was announced.
    /// </summary>
    public DateTime Announced { get; internal set; }
    /// <summary>
    /// After the time in this field, the resource will not be available from the general listing endpoint of the resource type, and it can not be used in new resources. For example, if this is an image, you can not create new servers with this image after the mentioned date.
    /// </summary>
    public DateTime UnavailableAfter { get; internal set; }
}