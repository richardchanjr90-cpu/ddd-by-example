using System.Collections.Generic;
using Loyalty.Application.ViewModels.Location;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Venue
{
    public class CreateVenueViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fullDescription")]
        public string FullDescription { get; set; }

        [JsonPropertyName("phones")]
        public List<string> Phones { get; set; }

        [JsonPropertyName("webSites")]
        public List<string> WebSites { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        [JsonPropertyName("workingHours")]
        public List<WorkingHoursViewModel> WorkingHours { get; set; }

        [JsonPropertyName("socialNetworks")]
        public SocialNetworksViewModel SocialNetworks { get; set; }

        [JsonPropertyName("parentId")]
        public long? ParentId { get; set; }

        [JsonPropertyName("location")]
        public LocationViewModel Location { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("categoryType")]
        public int CategoryType { get; set; }

        [JsonPropertyName("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("isPublished")]
        public bool IsPublished { get; set; }

        [JsonPropertyName("isApproved")]
        public bool IsApproved { get; set; }
    }
}