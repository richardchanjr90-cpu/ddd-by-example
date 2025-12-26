using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ProductPurchaseViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
