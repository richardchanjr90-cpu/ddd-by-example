using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Notifications.Workers
{
    public class WorkerAddedToVenueNotification: IVenueIntegrationEventsNotification
    {
        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("role")]
        public VenueUserRole Role { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }
    }
}
