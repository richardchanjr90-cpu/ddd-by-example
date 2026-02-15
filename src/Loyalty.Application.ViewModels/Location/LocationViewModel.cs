using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Location
{
    public class LocationViewModel
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("latitude")]
        public float Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public float Longitude { get; set; }
    }
}