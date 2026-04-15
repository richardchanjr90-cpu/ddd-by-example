using System;
using System.Text.Json.Serialization;

namespace Loyalty.Core.Entities.Aggregates.Venues.ValueObjects
{
    public class SocialNetworks
    {
        public SocialNetworks(
            Uri instagram, 
            Uri facebook, 
            Uri vkontakte)
        {
            Instagram = instagram;
            Facebook = facebook;
            Vkontakte = vkontakte;
        } 

        [JsonPropertyName("instagram")]
        public Uri Instagram { get; private set; }

        [JsonPropertyName("facebook")]
        public Uri Facebook { get; private set; }

        [JsonPropertyName("vkontakte")]
        public Uri Vkontakte { get; private set; }
    }
}
