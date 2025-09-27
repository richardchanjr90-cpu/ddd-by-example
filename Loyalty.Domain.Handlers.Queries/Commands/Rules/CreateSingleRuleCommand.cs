using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Domain.Handlers.Queries.Commands.Rules
{
    public class CreateSingleRuleCommand
    {
        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string RuleVersion { get; set; }
    }
}
