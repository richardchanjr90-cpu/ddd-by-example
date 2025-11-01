using System;
using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Purchase
{
    public class GetActivePurchaseResult
    {
        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<GroupPurchaseResult> Groups { get; set; } = new List<GroupPurchaseResult>();
    }
}
