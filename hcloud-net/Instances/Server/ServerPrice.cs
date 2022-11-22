namespace lkcode.hetznercloudapi.Instances.Server;

public class ServerPrice
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public string Location { get; private set; } = string.Empty;
    public Price PriceGross { get; private set; } = null;
}
