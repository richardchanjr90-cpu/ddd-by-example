using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
