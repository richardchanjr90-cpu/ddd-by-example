using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Signup
{
    public class SignupViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
