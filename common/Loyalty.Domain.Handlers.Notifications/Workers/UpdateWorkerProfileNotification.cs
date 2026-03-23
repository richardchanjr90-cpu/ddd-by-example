using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;

namespace Loyalty.Domain.Handlers.Notifications.Workers
{
    public class UpdateWorkerProfileNotification: IIntegrationEventsNotification
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }
}
