using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateSingleRuleCommand
    {
        [JsonPropertyName("rule")]
        public object Rule { get; set; }

        [JsonPropertyName("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonPropertyName("ruleVersion")]
        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}