using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Workers
{
    public class UpdatedWorkerNotification: IIntegrationEventsNotification
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("photoUri")]
        public string PhotoUri { get; set; }

        [JsonPropertyName("role")]
        public VenueUserRole Role { get; set; }
    }
}
