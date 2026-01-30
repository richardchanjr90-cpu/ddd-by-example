using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Application.ViewModels.Product;

namespace Loyalty.Application.ViewModels.ProductGroup
{
    public class ProductGroupViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public int Icon { get; set; }

        [JsonPropertyName("products")]
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
