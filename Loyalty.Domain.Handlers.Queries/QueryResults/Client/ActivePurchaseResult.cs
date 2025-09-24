using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Client
{
    public class ActivePurchaseResult
    {
        public long Id { get; set; }

        public decimal? Value { get; set; }
    }
}
