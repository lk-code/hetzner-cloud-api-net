namespace lkcode.hetznercloudapi.Objects.Server.GetRequestConsole
{
    public class Response
    {
        public string wss_url { get; set; }
        public string password { get; set; }
        public Objects.Server.Universal.ServerAction action { get; set; }
    }
}