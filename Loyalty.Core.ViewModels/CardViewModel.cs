using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class CardViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("loyaltyProgramId")]
        public int LoyaltyProgramId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
