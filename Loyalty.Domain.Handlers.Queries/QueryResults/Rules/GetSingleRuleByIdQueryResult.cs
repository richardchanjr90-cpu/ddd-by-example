using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Rules
{
    public class GetSingleRuleByIdQueryResult
    {
        public long Id { get; set; }

        public object Rule { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public LoyaltyRuleVersion RuleVersion { get; set; }
    }
}