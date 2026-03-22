using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Purchases
{
    public class BurnPurchaseNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("loyaltyProductGroupId")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        [JsonPropertyName("when")]
        public DateTime When { get; set; }

        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }
    }
}