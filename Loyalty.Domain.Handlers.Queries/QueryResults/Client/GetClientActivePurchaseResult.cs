using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Client
{
    public class GetClientActivePurchaseResult
    {
        public long LoyaltyProgramId { get; set; }

        public long LoyaltyGroupId { get; set; }

        public LoyaltyRuleType RuleType { get; set; }

        public List<ActivePurchaseResult> Purchases { get; set; } = new List<ActivePurchaseResult>();

        public Guid UserId { get; set; }
    }
}
