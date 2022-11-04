namespace lkcode.hetznercloudapi.Objects.Server.Create
{
    public class Response
    {
        public Server.Universal.Server server { get; set; }
        public Server.Universal.ServerAction action { get; set; }
        public string root_password { get; set; }
    }
}
