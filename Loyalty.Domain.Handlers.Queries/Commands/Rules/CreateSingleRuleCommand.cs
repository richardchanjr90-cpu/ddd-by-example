using Loyalty.Shared.Contracts.Enums;
using Newtonsoft.Json;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateSingleRuleCommand
    {
        [JsonProperty("rule")]
        public object Rule { get; set; }

        [JsonProperty("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonProperty("ruleVersion")]
        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}