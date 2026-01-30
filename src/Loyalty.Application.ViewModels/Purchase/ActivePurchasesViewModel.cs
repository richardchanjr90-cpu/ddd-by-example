using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ActivePurchasesViewModel
    {
        [JsonPropertyName("loyaltyProgramId")]
        public long LoyaltyProgramId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("groups")]
        public List<GroupPurchaseViewModel> Groups { get; set; } = new List<GroupPurchaseViewModel>();
    }
}
