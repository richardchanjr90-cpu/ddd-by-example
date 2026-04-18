using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.UserProfile
{
    public class UserProfileViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }
    }
}
