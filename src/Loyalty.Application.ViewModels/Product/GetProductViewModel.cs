using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Product
{
    public class GetProductViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("isAvailableForOrder")]
        public bool IsAvailableForOrder { get; set; }

        [JsonPropertyName("externalUid")]
        public string ExternalUid { get; set; }

        [JsonPropertyName("imageUri")]
        public string ImageUri { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public int Icon { get; set; }
    }
}
