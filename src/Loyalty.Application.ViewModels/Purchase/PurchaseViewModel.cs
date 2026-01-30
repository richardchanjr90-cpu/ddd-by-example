using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        [JsonPropertyName("loyaltyProductGroupId")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonPropertyName("productId")]
        public long? ProductId { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }
}
