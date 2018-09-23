namespace lkcode.hetznercloudapi.Api
{
    /// <summary>
    /// Resources the action relates to.
    /// </summary>
    public class ActionResource
    {
        /// <summary>
        /// ID of resource referenced.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type of resource referenced.
        /// </summary>
        public string Type { get; set; }
    }
}