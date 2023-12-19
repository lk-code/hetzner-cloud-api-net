namespace Hetzner.Cloud.Models;

public class PriceValue(string gross, string net)
{
    /// <summary>
    /// Price with VAT added
    /// </summary>
    public string Gross { get; } = gross;
    /// <summary>
    /// Price without VAT
    /// </summary>
    public string Net { get; } = net;
}