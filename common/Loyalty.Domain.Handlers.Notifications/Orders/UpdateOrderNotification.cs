using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Orders
{
    public class UpdateOrderNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
    }
}
