using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Orders
{
    public class GetOrderByVenueIdQueryResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("orderItems")]
        public List<GetOrderItemByVenueIdQueryResult> OrderItems { get; set; } 
            = new List<GetOrderItemByVenueIdQueryResult>();

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("placedDate")]
        public DateTime PlacedDate { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("pickUpTime")]
        public DateTime? PickUpTime { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("rate")]
        public string Rate { get; set; }
    }
}
