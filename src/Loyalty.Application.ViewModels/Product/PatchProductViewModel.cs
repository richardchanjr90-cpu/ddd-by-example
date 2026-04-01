using System;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Product
{
    public class PatchProductViewModel
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("isAvailableForOrder")]
        public bool IsAvailableForOrder { get; set; }
    }
}
