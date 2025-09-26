using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("venueId")]
        public string VenueId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
