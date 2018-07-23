namespace CloudApiNet.Api
{
    public class Network
    {
        public AddressIpv4 Ipv4 { get; set; }
        
        public AddressIpv6 Ipv6 { get; set; }
    }

    public class AddressIpv6
    {
        public string Ip { get; set; }
        
        public bool Blocked { get; set; }
    }

    public class AddressIpv4
    {
        public string Ip { get; set; }
        
        public bool Blocked { get; set; }
    }
}