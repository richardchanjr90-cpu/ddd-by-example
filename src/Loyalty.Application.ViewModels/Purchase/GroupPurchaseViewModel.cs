using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class GroupPurchaseViewModel
    {
        [JsonPropertyName("id")]
        public long LoyaltyProductGroupId { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("rule")]
        public string Rule { get; set; }

        [JsonPropertyName("ruleType")]
        public int RuleType { get; set; }

        [JsonPropertyName("ruleVersion")]
        public int RuleVersion { get; set; }

        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        [JsonPropertyName("products")]
        public List<ProductPurchaseViewModel> Products { get; set; } = new List<ProductPurchaseViewModel>();
    }
}
