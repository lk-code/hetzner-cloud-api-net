namespace lkcode.hetznercloudapi.Api
{
    public class FloatingIpHomeLocation
    {
        /// <summary>
        /// ID of the location.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the location.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-2 code of the country the location resides in.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// City the location is closest to.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Latitude of the city closest to the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of the city closest to the location.
        /// </summary>
        public double Longitude { get; set; }
    }
}
