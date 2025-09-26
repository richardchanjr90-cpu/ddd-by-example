using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("loyaltyProductGroupId")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
