using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Workers
{
    public class PatchWorkerNotification: IIntegrationEventsNotification
    {
        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("photoUri")]
        public string PhotoUri { get; set; }
    }
}