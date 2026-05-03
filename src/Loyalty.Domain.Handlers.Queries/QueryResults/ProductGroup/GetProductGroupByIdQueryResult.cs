using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup
{
    public class GetProductGroupByIdQueryResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public ProductGroupIconType Icon { get; set; }

        [JsonPropertyName("isAvailableForOrder")]
        public bool IsAvailableForOrder { get; set; }

        [JsonPropertyName("products")]
        public List<GetProductByIdQueryResult> Products { get; set; } = new List<GetProductByIdQueryResult>();
    }
}