using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Workers
{
    public class SetupFirebaseTokenNotification: IVenueIntegrationEventsNotification
    {
        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("venueRoles")]
        public Dictionary<string, VenueUserRole> VenueRoles { get; set; } = new Dictionary<string, VenueUserRole>();
    }
}
