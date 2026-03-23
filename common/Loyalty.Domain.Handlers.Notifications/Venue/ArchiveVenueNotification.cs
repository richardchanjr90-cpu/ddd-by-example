using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class ArchiveVenueNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}