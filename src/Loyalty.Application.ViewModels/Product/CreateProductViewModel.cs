using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [JsonPropertyName("productGroupId")]
        public long ProductGroupId { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("externalUid")]
        public string ExternalUid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public int Icon { get; set; }
    }
}
