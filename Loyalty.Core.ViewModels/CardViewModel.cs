using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class CardViewModel
    {
        [JsonProperty("id")]
        public Guid ItemId { get; set; }

        [JsonProperty("loyaltyProgramId")]
        public int LoyaltyProgramId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
