using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class AddUserToVenueNotification : IVenueIntegrationEventsNotification
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venueIds")]
        public List<string> VenueIds { get; set; } = new List<string>();
    }
}
