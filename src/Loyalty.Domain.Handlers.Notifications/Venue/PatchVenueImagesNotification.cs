using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueImagesNotification : INotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("images")]
        public string Images { get; set; }
    }
}
