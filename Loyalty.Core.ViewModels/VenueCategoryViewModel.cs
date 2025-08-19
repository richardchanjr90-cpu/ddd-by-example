using System;
using Loyalty.Core.Shared.Enums;
using Newtonsoft.Json;

namespace Loyalty.Core.ViewModels
{
    public class VenueCategoryViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("categoryType")]
        public VenueCategoryType CategoryType { get; set; }
    }
}
