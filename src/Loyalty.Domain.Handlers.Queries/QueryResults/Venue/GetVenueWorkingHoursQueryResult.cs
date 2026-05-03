using System;
using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenueWorkingHoursQueryResult
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("from")]
        public int? From { get; set; }

        [JsonPropertyName("to")]
        public int? To { get; set; }
    }
}