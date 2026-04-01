using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Orders
{
    public class GetOrderItemByVenueIdQueryResult
    {
        public int Amount { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public Uri ImageUrl { get; set; }
    }
}
