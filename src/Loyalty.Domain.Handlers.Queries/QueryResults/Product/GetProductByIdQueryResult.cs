using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Product
{
    public class GetProductByIdQueryResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public ProductIconType? Icon { get; set; }

        [JsonPropertyName("productGroupId")]
        public long ProductGroupId { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("isAvailableForOrder")]
        public bool IsAvailableForOrder { get; set; }

        [JsonPropertyName("externalUid")]
        public string ExternalUid { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("imageUri")]
        public string ImageUri { get; set; }

        [JsonPropertyName("isArchived")]
        public bool IsArchived { get; set; }
    }
}
