using System.Collections.Generic;
using Loyalty.Application.ViewModels.Location;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Venue
{
    public class UpdateVenueViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fullDescription")]
        public string FullDescription { get; set; }

        [JsonProperty("phones")]
        public List<string> Phones { get; set; }

        [JsonProperty("webSites")]
        public List<string> WebSites { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("workingHours")]
        public List<WorkingHoursViewModel> WorkingHours { get; set; }

        [JsonProperty("parentId")]
        public long? ParentId { get; set; }

        [JsonProperty("location")]
        public LocationViewModel Location { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("categoryType")]
        public int CategoryType { get; set; }

        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonProperty("socialNetworks")]
        public SocialNetworksViewModel SocialNetworks { get; set; }

        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [JsonProperty("isApproved")]
        public bool IsApproved { get; set; }
    }
}