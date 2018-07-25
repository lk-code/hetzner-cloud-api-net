namespace HetznerCloudNet.Objects.Server.Universal
{
    public class Error
    {
        public string message { get; set; }
        public string code { get; set; }
        public object details { get; set; }
    }

    public class ErrorResponse
    {
        public Error error { get; set; }
    }
}
