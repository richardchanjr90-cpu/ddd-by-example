using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class AddUserToVenueNotification : IVenueIntegrationEventsNotification
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venueRoles")]
        public Dictionary<string, VenueUserRole> VenueRoles { get; set; } = new Dictionary<string, VenueUserRole>();
    }
}
