using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class ArchiveVenueNotification : INotification
    {
        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}