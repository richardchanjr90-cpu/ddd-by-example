using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.ProductGroup
{
    public class ProductGroupViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("venueId")]
        public long VenueId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
