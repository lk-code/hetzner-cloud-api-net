namespace Hetzner.Cloud.Models;

public class Ip6Address(long id, string ip, bool blocked, IpDnsPointer[] dnsPointer)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// IP address (v6) of this Server
    /// </summary>
    public string Ip { get; } = ip;
    /// <summary>
    /// If the IP is blocked by our anti abuse dept
    /// </summary>
    public bool Blocked { get; } = blocked;
    /// <summary>
    /// Reverse DNS PTR entry for the IPv4 addresses of this Server
    /// </summary>
    public IpDnsPointer[] DnsPointer { get; } = dnsPointer;
}