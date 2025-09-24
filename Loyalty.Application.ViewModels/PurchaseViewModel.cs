using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class PurchaseViewModel
    {
        [JsonProperty("loyaltyGroupId")]
        public long LoyaltyGroupId { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}
