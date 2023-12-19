namespace Hetzner.Cloud.Models;

public class IpDnsPointer(string ip, string dnsPointer)
{
    /// <summary>
    /// IP address (v4 or v6) of this Server
    /// </summary>
    public string Ip { get; } = ip;
    /// <summary>
    /// IP address (v4 or v6) of this Server
    /// </summary>
    public string DnsPointer { get; } = dnsPointer;
}