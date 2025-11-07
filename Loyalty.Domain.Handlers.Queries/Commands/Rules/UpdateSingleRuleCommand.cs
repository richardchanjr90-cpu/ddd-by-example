using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class UpdateSingleRuleCommand : IRequest<ICommandResult>
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("rule")] public object Rule { get; set; }

        [JsonProperty("ruleType")] public LoyaltyRuleType RuleType { get; set; }

        [JsonProperty("ruleVersion")] public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}