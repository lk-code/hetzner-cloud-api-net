namespace Hetzner.Cloud.Models;

public class Price(string location, PriceValue hourly, PriceValue monthly)
{
    /// <summary>
    /// Name of the Location the price is for
    /// </summary>
    public string Location { get; } = location;
    /// <summary>
    /// Monthly costs for a Server type in this Location
    /// </summary>
    public PriceValue Hourly { get; } = hourly;
    /// <summary>
    /// Monthly costs for a Server type in this Location
    /// </summary>
    public PriceValue Monthly { get; } = monthly;
}