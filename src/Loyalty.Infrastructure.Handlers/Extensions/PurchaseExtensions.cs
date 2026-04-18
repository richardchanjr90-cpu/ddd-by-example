using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class PurchaseExtensions
    {
        public static List<GroupPurchaseResult> ToResult(this IEnumerable<Purchase> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = items
                .Select(item => new GroupPurchaseResult
                {
                    // Id = item.Id,
                    LoyaltyProductGroupId = item.LoyaltyProductGroupId
                    // Value = item.Value
                }).ToList();

            return results;
        }

        public static GroupPurchaseResult ToResult(this Purchase item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var results = new GroupPurchaseResult
            {
                //Id = item.Id,
                LoyaltyProductGroupId = item.LoyaltyProductGroupId
                //Value = item.Value
            };

            return results;
        }
    }
}