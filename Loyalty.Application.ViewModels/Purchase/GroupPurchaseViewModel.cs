using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class GroupPurchaseViewModel
    {
        [JsonProperty("id")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("rule")]
        public string Rule { get; set; }

        [JsonProperty("ruleType")]
        public int RuleType { get; set; }

        [JsonProperty("ruleVersion")]
        public string RuleVersion { get; set; }

        [JsonProperty("total")]
        public decimal? Total { get; set; }

        [JsonProperty("products")]
        public List<ProductPurchaseViewModel> Products { get; set; } = new List<ProductPurchaseViewModel>();
    }
}
