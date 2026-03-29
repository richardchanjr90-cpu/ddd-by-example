using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class PatchVenueLastProgramCreateDateNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("lastStartedProgramDate")]
        public DateTime LastCreatedProgramDate { get; set; }
    }
}
