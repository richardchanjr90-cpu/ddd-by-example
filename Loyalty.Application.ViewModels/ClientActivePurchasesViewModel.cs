using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class ClientActivePurchasesViewModel
    {
        public long LoyaltyProgramId { get; set; }

        public long LoyaltyGroupId { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public List<ActivePurchaseViewModel> Purchases { get; set; } = new List<ActivePurchaseViewModel>();

        public Guid UserId { get; set; }



        [JsonProperty("sum")]
        public string Sum { get; set; }
    }
}
