using System;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ProductPurchaseViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
