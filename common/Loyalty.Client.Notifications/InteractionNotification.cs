using System;
using System.Text.Json.Serialization;
using Loyalty.Client.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Client.Notifications
{
    public class InteractionNotification : IClientStatisticsNotification
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("interactionType")]
        public UserInteractionType InteractionType { get; set; }

        [JsonPropertyName("when")]
        public DateTime When { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }
    }
}
