using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Application.ViewModels.ClientInfo;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ClientInfoPurchasesViewModel
    {
        [JsonPropertyName("clientInfo")]
        public ClientInfoViewModel ClientInfo { get; set; }

        [JsonPropertyName("activePurchases")]
        public List<ActivePurchasesViewModel> ActivePurchases { get; set; } = new List<ActivePurchasesViewModel>();

        [JsonPropertyName("orders")]
        public List<GetOrderByUserIdQueryResult> Orders { get; set; } = new List<GetOrderByUserIdQueryResult>();
    }
}