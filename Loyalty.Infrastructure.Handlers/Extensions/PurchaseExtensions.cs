using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class PurchaseExtensions
    {
        public static List<ActivePurchaseResult> ToResult(this IEnumerable<Purchase> items)
        {
            var results = new List<ActivePurchaseResult>();
            if (items != null)
            {
                results = items
                    .Select(item => new ActivePurchaseResult
                    {
                        Id = item.Id,
                        Value = item.Value
                    }).ToList();
            }

            return results;
        }
    }
}
