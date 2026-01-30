using System;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Venue
{
    public class WorkingHoursViewModel
    {
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("from")]
        public int? From { get; set; }

        [JsonPropertyName("to")]
        public int? To { get; set; }
    }
}
