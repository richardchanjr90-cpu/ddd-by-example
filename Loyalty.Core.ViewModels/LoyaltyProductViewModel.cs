using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class LoyaltyProductViewModel
    {
        [JsonProperty("id")]
        public Guid ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isArchived")]
        public bool IsArchived { get; set; }

        [JsonProperty("stampsToCollectCount")]
        public int StampsToCollectCount { get; set; }
    }
}
