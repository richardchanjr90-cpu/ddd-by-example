using System;
using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class WorkingHoursCommand
    {
        [JsonPropertyName("day")]
        public DayOfWeek Day { get; set; }

        [JsonPropertyName("from")]
        public int? From { get; set; }

        [JsonPropertyName("to")]
        public int? To { get; set; }
    }
}
