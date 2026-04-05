using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Rate
{
    public class RateViewModel
    {
        [JsonPropertyName("rate")]
        public int Rate { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}
