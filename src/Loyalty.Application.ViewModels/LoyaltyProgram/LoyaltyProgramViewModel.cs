using System;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.LoyaltyProgram
{
    public class LoyaltyProgramViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("isPublished")]
        public bool IsPublished { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("externalProgramUri")]
        public string ExternalProgramUri { get; set; }
    }
}
