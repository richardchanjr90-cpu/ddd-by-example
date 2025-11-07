using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Purchase
{
    public class GroupPurchaseResult
    {
        public long LoyaltyProductGroupId { get; set; }

        public string GroupName { get; set; }

        public string Rule { get; set; }

        public int RuleType { get; set; }

        public int RuleVersion { get; set; }

        public decimal? Total { get; set; }

        public List<ProductPurchaseResult> Products { get; set; } = new List<ProductPurchaseResult>();
    }
}