using System;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class CreateVenueNotification : IIntegrationEventsNotification
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("latitude")]
        public float? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public float? Longitude { get; set; }

        [JsonPropertyName("fullDescription")]
        public string FullDescription { get; set; }

        [JsonPropertyName("phones")]
        public string Phones { get; set; }

        [JsonPropertyName("webSites")]
        public string WebSites { get; set; }

        [JsonPropertyName("workingHours")]
        public string WorkingHours { get; set; }

        [JsonPropertyName("parentId")]
        public long? ParentId { get; set; }

        [JsonPropertyName("type")]
        public VenueType Type { get; set; }

        [JsonPropertyName("categoryType")]
        public VenueCategoryType CategoryType { get; set; }

        [JsonPropertyName("socialNetworks")]
        public string SocialNetworks { get; set; }

        [JsonPropertyName("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("isArchived")]
        public bool IsArchived { get; set; }

        [JsonPropertyName("isPublished")]
        public bool IsPublished { get; set; }

        [JsonPropertyName("isApproved")]
        public bool IsApproved { get; set; }
    }
}