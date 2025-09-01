using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class VenueViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

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

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [JsonProperty("isApproved")]
        public bool IsApproved { get; set; }
    }
}


