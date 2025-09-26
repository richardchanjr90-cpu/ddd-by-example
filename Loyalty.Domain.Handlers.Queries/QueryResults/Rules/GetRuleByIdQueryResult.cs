using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Rules
{
    public class GetRuleByIdQueryResult
    {
        public long Id { get; set; }

        public string Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public string RuleVersion { get; set; }
    }
}
