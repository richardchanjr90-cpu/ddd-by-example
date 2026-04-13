using System;
using System.Text.Json.Serialization;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetSocialNetworksResult
    {
        [JsonPropertyName("instagram")]
        public Uri Instagram { get; set; }

        [JsonPropertyName("facebook")]
        public Uri Facebook { get; set; }
       
        [JsonPropertyName("vkontakte")]
        public Uri Vkontakte { get; set; }
    }
}
