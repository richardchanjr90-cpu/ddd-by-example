using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Loyalty.Application.ViewModels.Worker
{
    public class UserProfileViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("positionName")]
        public string PositionName { get; set; }
    }
}
