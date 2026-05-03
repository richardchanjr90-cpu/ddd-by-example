using System;
using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Orders
{
    public class GetOrdersByVenueIdQueryResult
    {
        public List<GetOrderByVenueIdQueryResult> Orders { get; set; } = new List<GetOrderByVenueIdQueryResult>();
    }
}
