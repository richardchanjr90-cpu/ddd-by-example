using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Rate
{
    public class UpsertUserRateNotification: IUserEventsNotification
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("ownerId")]
        public long OrderId { get; set; }

        [JsonPropertyName("rate")]
        public int Rate { get; set; }
    }
}
