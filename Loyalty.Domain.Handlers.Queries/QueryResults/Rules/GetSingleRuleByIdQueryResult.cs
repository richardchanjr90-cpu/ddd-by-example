using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums.Contracts;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Rules
{
    public class GetSingleRuleByIdQueryResult
    {
        public long Id { get; set; }

        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string RuleVersion { get; set; }
    }
}
