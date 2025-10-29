using System;
using Loyalty.Common.Shared.Enums.Contracts;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateSingleRuleCommand
    {
        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string RuleVersion { get; set; }
    }
}
