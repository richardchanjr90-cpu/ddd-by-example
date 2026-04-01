using System;
using System.Collections.Generic;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Orders
{
    public class GetOrderByVenueIdQueryResult
    {
        public long Id { get; set; }

        public List<GetOrderItemByVenueIdQueryResult> OrderItems { get; set; } 
            = new List<GetOrderItemByVenueIdQueryResult>();

        public long VenueId { get; set; }

        public long MenuId { get; set; }

        public DateTime PlacedDate { get; set; }

        public string CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime? PickUpTime { get; set; }

        public string Comment { get; set; }
    }
}
