using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueApproveNotification : INotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
