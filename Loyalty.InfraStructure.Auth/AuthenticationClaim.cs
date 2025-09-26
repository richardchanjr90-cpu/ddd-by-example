using Newtonsoft.Json;

namespace Loyalty.InfraStructure.Auth
{
    public class AuthenticationClaim
    {
        public AuthenticationClaim()
        {
        }

        public AuthenticationClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        [JsonProperty("typ")]
        public string Type { get; set; }

        [JsonProperty("val")]
        public string Value { get; set; }
    }
}
