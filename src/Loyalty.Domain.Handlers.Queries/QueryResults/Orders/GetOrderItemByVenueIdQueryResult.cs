using System;
using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Orders
{
    public class GetOrderItemByVenueIdQueryResult
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("productId")]
        public long ProductId { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
