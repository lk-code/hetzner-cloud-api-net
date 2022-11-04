namespace lkcode.hetznercloudapi.Api
{
    /// <summary>
    /// The Data for the requested server console.
    /// </summary>
    public class ServerConsoleData
    {
        /// <summary>
        /// URL of websocket proxy to use. This includes a token which is valid for a limited time only.
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// VNC password to use for this connection. This password only works in combination with a wss_url with valid token.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}