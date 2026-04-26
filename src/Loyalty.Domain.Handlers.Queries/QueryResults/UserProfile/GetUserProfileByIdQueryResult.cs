using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile
{
    public class GetUserProfileByIdQueryResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("photoUri")]
        public string PhotoUri { get; set; }
    }
}