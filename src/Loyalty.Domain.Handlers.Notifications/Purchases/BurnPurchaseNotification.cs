using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Purchases
{
    public class BurnPurchaseNotification : INotification
    {
        [JsonPropertyName("loyaltyProductGroupId")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("total")]
        public decimal? Total { get; set; }
    }
}