namespace Hetzner.Cloud.Models;

public class Ip4Address(long id, string ip, bool blocked, string dnsPointer)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// IP address (v4) of this Server
    /// </summary>
    public string Ip { get; } = ip;
    /// <summary>
    /// If the IP is blocked by our anti abuse dept
    /// </summary>
    public bool Blocked { get;  } = blocked;
    /// <summary>
    /// Reverse DNS PTR entry for the IPv4 addresses of this Server
    /// </summary>
    public string DnsPointer { get; } = dnsPointer;
}