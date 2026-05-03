using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Products
{
    public class CreateProductNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("groupIcon")]
        public ProductGroupIconType GroupIcon { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
