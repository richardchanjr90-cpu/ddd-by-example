
using System.Text.Json.Serialization;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class UpdateSingleRuleCommand : IRequest<ICommandResult>
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonPropertyName("rule")]
        public object Rule { get; set; }

        [JsonPropertyName("ruleType")]
        public LoyaltyRuleType RuleType { get; set; }

        [JsonPropertyName("ruleVersion")]
        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}