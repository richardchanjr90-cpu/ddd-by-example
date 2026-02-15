using System;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.ClientInfo
{
    public class ClientInfoViewModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("photoUrl")]
        public string PhotoUrl { get; set; }
    }
}
