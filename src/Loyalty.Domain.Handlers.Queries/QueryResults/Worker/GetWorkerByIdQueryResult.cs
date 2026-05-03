using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetWorkerByIdQueryResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venues")]
        public List<GetVenueWorkerResult> Venues { get; set; } = new List<GetVenueWorkerResult>();

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