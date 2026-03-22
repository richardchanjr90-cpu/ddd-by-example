using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueLogoNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }
}
