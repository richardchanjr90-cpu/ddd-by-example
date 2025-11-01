using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        [JsonProperty("loyaltyProductGroupId")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonProperty("productId")]
        public long? ProductId { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
