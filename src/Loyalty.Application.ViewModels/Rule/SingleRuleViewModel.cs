using System;
using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Rule
{
    public class SingleRuleViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("rule")]
        public string Rule { get; set; }

        [JsonPropertyName("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonPropertyName("ruleVersion")]
        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}
