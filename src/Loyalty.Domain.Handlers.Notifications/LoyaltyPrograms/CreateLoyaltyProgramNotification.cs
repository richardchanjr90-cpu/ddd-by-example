using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms
{
    public class CreateLoyaltyProgramNotification : INotification
    {        
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("venueId")]
        public long VenueId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("isPublished")]
        public bool IsPublished { get; set; }
    }
}