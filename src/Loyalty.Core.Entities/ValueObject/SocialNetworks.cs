using System;
using Newtonsoft.Json;

namespace Loyalty.Core.Entities.ValueObject
{
    public class SocialNetworks
    {
        [JsonProperty("instagram")]
        public Uri Instagram { get; set; }

        [JsonProperty("facebook")]
        public Uri Facebook { get; set; }

        [JsonProperty("vkontakte")]
        public Uri Vkontakte { get; set; }
    }
}
