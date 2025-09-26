using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Location
{
    public class LocationViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("venueId")]
        public long VenueId { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }
    }
}