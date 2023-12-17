namespace Hetzner.Cloud.Models;

public class PrivateNetwork(string ip, string macAddress, long network, string[] aliasIps)
{
    public string Ip { get; } = ip;
    public string MacAddress { get; } = macAddress;
    public long Network { get; } = network;
    public string[] AliasIps { get; } = aliasIps;
}