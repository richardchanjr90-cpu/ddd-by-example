using System;
using System.Text.Json.Serialization;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObject
{
    public class SocialNetworks
    {
        [JsonPropertyName("instagram")]
        public Uri Instagram { get; set; }

        [JsonPropertyName("facebook")]
        public Uri Facebook { get; set; }

        [JsonPropertyName("vkontakte")]
        public Uri Vkontakte { get; set; }
    }
}
