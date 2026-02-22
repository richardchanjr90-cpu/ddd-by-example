using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Worker
{
    public class VenueWorkerViewModel
    {
        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }

        [JsonPropertyName("role")]
        public int Role { get; set; }
    }
}
