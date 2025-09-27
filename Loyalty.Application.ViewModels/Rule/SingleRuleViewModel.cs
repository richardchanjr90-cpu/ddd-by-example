using System;
using Loyalty.Common.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Rule
{
    public class SingleRuleViewModel
    {
        [JsonProperty("rule")]
        public string Rule { get; set; }

        [JsonProperty("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonProperty("ruleVersion")]
        public string RuleVersion { get; set; }
    }
}
