using System;
using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Orders
{
    public class GetOrdersByUserIdQueryResult
    {
        public List<GetOrderByUserIdQueryResult> Orders { get; set; } = new List<GetOrderByUserIdQueryResult>();
    }
}
