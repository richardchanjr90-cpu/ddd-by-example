using System;
using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateSingleRuleCommand
    {
        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}
