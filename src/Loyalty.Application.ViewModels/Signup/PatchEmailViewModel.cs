using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Signup
{
    public class PatchEmailViewModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
