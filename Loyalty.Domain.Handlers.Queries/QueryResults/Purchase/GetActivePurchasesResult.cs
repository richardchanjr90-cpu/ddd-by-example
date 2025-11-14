using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Purchase
{
    public class GetActivePurchasesResult
    {
        public List<GetActivePurchaseResult> Result { get; set; } = new List<GetActivePurchaseResult>();
    }
}