using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels
{
    public class ClientViewModel
    {
        [JsonProperty("userCode")]
        public string UserCode { get; set; }

        [JsonProperty("profileUri")]
        public string ProfileUri { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("sum")]
        public string Sum { get; set; }

        [JsonProperty("purchasesAmount")]
        public string PurchasesAmount { get; set; }
    }
}
