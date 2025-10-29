using System.Collections.Generic;
using Loyalty.Common.Shared.Enums.Contracts;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupByIdQueryResult
    {
        public long Id { get; set; }

        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public GetRuleByIdQueryResult Rules { get; set; }

        public GetProductGroupByIdQueryResult ProductGroup { get; set; }

        public string Description { get; set; }

        public bool IsArchived { get; set; }
    }
}
