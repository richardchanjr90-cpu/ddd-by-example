using System;
using Newtonsoft.Json;

namespace Loyalty.Application.ViewModels.Worker
{
    public class WorkerViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("workerId")]
        public string WorkerId { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("photoUri")]
        public string PhotoUri { get; set; }

        [JsonProperty("positionName")]
        public string PositionName { get; set; }
        
    }
}
