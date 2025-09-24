using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Purchase
{
    public class ActivePurchaseViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("value")]
        public decimal? Value { get; set; }
    }
}
