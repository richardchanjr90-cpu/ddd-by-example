using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueLogoNotification : INotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }
}
