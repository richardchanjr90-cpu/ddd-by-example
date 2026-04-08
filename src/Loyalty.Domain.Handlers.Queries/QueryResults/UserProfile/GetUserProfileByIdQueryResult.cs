using System.Collections.Generic;
using System.Text.Json.Serialization;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile
{
    public class GetUserProfileByIdQueryResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("photoUri")]
        public string PhotoUri { get; set; }
    }
}