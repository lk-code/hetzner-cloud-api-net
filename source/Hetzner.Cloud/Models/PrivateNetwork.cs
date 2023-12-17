namespace Hetzner.Cloud.Models;

public class PrivateNetwork(string ip, string macAddress, long network, string[] aliasIps)
{
    public string Ip { get; internal set; } = ip;
    public string MacAddress { get; internal set; } = macAddress;
    public long Network { get; internal set; } = network;
    public string[] AliasIps { get; internal set; } = aliasIps;
}