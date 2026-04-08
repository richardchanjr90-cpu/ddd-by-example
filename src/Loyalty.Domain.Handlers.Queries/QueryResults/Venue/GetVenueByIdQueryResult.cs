using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenueByIdQueryResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fullDescription")]
        public string FullDescription { get; set; }

        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }

        [JsonPropertyName("phones")] 
        public List<string> Phones { get; set; } = new List<string>();

        [JsonPropertyName("webSites")]
        public List<string> WebSites { get; set; } = new List<string>();

        [JsonPropertyName("images")]
        public List<string> Images { get; set; } = new List<string>();

        [JsonPropertyName("workingHours")]
        public List<GetVenueWorkingHoursQueryResult> WorkingHours { get; set; }            
            = new List<GetVenueWorkingHoursQueryResult>();

        [JsonPropertyName("parentId")]
        public long? ParentId { get; set; }

        [JsonPropertyName("location")]
        public GetLocationQueryResult Location { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("categoryType")]
        public long CategoryType { get; set; }

        [JsonPropertyName("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("socialNetworks")]
        public GetSocialNetworksResult SocialNetworks { get; set; } 

        [JsonPropertyName("venueApprovalStatus")]
        public int VenueApprovalStatus { get; set; }

        [JsonPropertyName("acceptsOrders")]
        public bool AcceptsOrders { get; set; }
    }
}