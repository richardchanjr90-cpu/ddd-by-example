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

        [JsonPropertyName("startedDate")]
        public DateTime StartedDate { get; set; }

        [JsonPropertyName("endedDate")]
        public DateTime EndedDate { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
