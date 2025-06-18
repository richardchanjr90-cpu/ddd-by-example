using System;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class StampViewModel
    {
        [JsonProperty("id")]
        public Guid ItemId { get; set; }

        [JsonProperty("cardId")]
        public int CardId { get; set; }

        [JsonProperty("stampedDate")]
        public DateTime StampedDate { get; set; }

        [JsonProperty("usedDate")]
        public DateTime? UsedDate { get; set; }
    }
}