namespace lkcode.hetznercloudapi.Objects.SshKey.Universal
{
    public class SshKey
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fingerprint { get; set; }
        public string public_key { get; set; }
    }
}
