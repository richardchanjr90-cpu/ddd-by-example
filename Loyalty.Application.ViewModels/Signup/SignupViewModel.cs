using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Signup
{
    public class SignupViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}
