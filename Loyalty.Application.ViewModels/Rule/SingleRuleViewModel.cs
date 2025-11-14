using System;
using Loyalty.Shared.Contracts.Enums;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Rule
{
    public class SingleRuleViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rule")]
        public string Rule { get; set; }

        [JsonProperty("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonProperty("ruleVersion")]
        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}
