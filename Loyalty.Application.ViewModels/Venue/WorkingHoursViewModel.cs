using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Venue
{
    public class WorkingHoursViewModel
    {
        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("from")]
        public int? From { get; set; }

        [JsonProperty("to")]
        public int? To { get; set; }
    }
}
