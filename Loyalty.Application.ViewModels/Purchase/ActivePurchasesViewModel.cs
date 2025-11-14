using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ActivePurchasesViewModel
    {
        [JsonProperty("loyaltyProgramId")]
        public long LoyaltyProgramId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("groups")]
        public List<GroupPurchaseViewModel> Groups { get; set; } = new List<GroupPurchaseViewModel>();
    }
}
