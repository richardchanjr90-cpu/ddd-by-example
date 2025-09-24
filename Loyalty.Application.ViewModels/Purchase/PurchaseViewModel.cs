using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("loyaltyGroupId")]
        public long LoyaltyGroupId { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}
