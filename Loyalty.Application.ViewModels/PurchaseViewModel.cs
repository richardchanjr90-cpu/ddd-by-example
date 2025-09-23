using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class PurchaseViewModel
    {
        [JsonProperty("userId")]
        public string UserId  { get; set; }

        [JsonProperty("userCode")]
        public string UserCode { get; set; }

        [JsonProperty("venueId")]
        public long VenueId { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("loyaltyProductId")]
        public long LoyaltyProductId { get; set; }
    }
}
