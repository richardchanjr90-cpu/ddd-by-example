using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Client
{
    public class GetClientActivePurchasesResult
    {
        public List<GetClientActivePurchaseResult> Result { get; set; } = new List<GetClientActivePurchaseResult>();
    }
}
