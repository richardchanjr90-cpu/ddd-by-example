using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Workers
{
    public class ArchiveWorkerNotification: IIntegrationEventsNotification
    {
        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }
    }
}
