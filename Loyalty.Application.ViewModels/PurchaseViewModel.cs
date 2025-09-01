using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class PurchaseViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("cardId")]
        public int CardId { get; set; }

        [JsonProperty("stampedDate")]
        public DateTime StampedDate { get; set; }

        [JsonProperty("usedDate")]
        public DateTime? UsedDate { get; set; }
    }
}