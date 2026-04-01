using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.ProductGroup
{
    public class ProductGroupPatchViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("isAvailableForOrder")]
        public bool IsAvailableForOrder { get; set; }
    }
}
