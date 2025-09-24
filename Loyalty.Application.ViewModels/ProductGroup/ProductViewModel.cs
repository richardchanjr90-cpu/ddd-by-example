using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.ProductGroup
{
    public class ProductGroupViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
