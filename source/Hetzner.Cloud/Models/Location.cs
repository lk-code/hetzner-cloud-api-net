namespace Hetzner.Cloud.Models;

public class Location(long id)
{
    /// <summary>
    /// ID of the Location
    /// </summary>
    public long Id { get; private set; } = id;
    /// <summary>
    /// City the Location is closest to
    /// </summary>
    public string City { get; internal set; } = string.Empty;
    /// <summary>
    /// ISO 3166-1 alpha-2 code of the country the Location resides in
    /// </summary>
    public string Country { get; internal set; } = string.Empty;
    /// <summary>
    /// Description of the Location
    /// </summary>
    public string Description { get; internal set; } = string.Empty;
    /// <summary>
    /// Latitude of the city closest to the Location
    /// </summary>
    public double Latitude { get; internal set; } = 0;
    /// <summary>
    /// Longitude of the city closest to the Location
    /// </summary>
    public double Longitude { get; internal set; } = 0;
    /// <summary>
    /// Unique identifier of the Location
    /// </summary>
    public string Name { get; internal set; } = string.Empty;
    /// <summary>
    /// Name of network zone this Location resides in
    /// </summary>
    public string NetworkZone { get; internal set; } = string.Empty;
}