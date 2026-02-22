using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Worker
{
    public class CreateWorkerViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }

        [JsonPropertyName("role")]
        public int Role { get; set; }

        [JsonPropertyName("workerId")]
        public string WorkerId { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("photoUri")]
        public string PhotoUri { get; set; }
    }
}
