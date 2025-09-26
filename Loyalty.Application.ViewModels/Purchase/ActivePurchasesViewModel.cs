using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ActivePurchasesViewModel
    {
        [JsonProperty("loyaltyProgramId")]
        public long LoyaltyProgramId { get; set; }

        [JsonProperty("loyaltyGroupId")]
        public long LoyaltyGroupId { get; set; }

        [JsonProperty("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonProperty("purchases")]
        public List<ActivePurchaseViewModel> Purchases { get; set; } = new List<ActivePurchaseViewModel>();

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
