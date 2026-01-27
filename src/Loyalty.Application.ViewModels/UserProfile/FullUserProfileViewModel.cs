using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Worker
{
    public class FullUserProfileViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("positionName")]
        public string PositionName { get; set; }

        [JsonProperty("photoUri")]
        public string PhotoUri { get; set; }
    }
}
