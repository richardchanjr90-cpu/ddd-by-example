using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.LoyaltyProductGroup
{
    public class LoyaltyProductGroupViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }
    }
}
