using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Worker
{
    public class UserProfileViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("positionName")]
        public string PositionName { get; set; }
    }
}
