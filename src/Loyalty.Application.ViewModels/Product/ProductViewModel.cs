using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public int Icon { get; set; }
    }
}
