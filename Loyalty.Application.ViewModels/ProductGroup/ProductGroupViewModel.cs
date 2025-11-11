using System.Collections.Generic;
using Loyalty.Application.ViewModels.Product;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.ProductGroup
{
    public class ProductGroupViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("venueId")]
        public long VenueId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public int Icon { get; set; }

        [JsonProperty("products")]
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
