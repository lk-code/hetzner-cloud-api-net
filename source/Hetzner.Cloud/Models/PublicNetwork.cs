namespace Hetzner.Cloud.Models;

public class PublicNetwork
{
    public Firewall[] Firewalls { get; internal set; } = Array.Empty<Firewall>();
    public long[] FloatingIps { get; internal set; } = Array.Empty<long>();
    public Ip4Address? Ipv4 { get; internal set; }
    public Ip6Address? Ipv6 { get; internal set; }
}