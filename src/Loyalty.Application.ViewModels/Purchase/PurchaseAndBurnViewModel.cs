using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class PurchaseAndBurnViewModel
    {
        [JsonPropertyName("loyaltyProductGroupId")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonPropertyName("productId")]
        public long? ProductId { get; set; }

        [JsonPropertyName("burn")]
        public decimal Burn { get; set; }

        [JsonPropertyName("purchase")]
        public decimal Purchase { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }
}
