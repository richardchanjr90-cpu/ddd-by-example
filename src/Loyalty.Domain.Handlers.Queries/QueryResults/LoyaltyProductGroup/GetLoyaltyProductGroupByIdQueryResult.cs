using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupByIdQueryResult
    {
        public long Id { get; set; }

        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public GetRuleByIdQueryResult Rules { get; set; }

        public GetProductGroupByIdQueryResult ProductGroup { get; set; }

        public string Description { get; set; }
    }
}