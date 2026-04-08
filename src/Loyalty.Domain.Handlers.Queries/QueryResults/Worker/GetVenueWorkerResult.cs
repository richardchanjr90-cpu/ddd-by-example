using System;
using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetVenueWorkerResult
    {
        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }

        [JsonPropertyName("role")]
        public VenueUserRole Role { get; set; }
    }
}
