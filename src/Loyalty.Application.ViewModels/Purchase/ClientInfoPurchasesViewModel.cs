using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Application.ViewModels.ClientInfo;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ClientInfoPurchasesViewModel
    {
        [JsonPropertyName("clientInfo")]
        public ClientInfoViewModel ClientInfo { get; set; }

        [JsonPropertyName("activePurchases")]
        public List<ActivePurchasesViewModel> ActivePurchases { get; set; } = new List<ActivePurchasesViewModel>();
    }
}