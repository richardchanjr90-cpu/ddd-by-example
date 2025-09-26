using System.Collections.Concurrent;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Settings;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Loyalty.InfraStructure.Auth.Validation
{
    public sealed class CachedTokenValidator : TokenValidator
    {
        private static readonly ConcurrentDictionary<string, OpenIdConnectConfiguration> CachedDictionary =
            new ConcurrentDictionary<string, OpenIdConnectConfiguration>();

        public CachedTokenValidator(AuthSettings settings) :
            base(settings)
        {
        }

        protected override async Task<OpenIdConnectConfiguration> GetOpenIdConfig(string url)
        {
            if (!CachedDictionary.TryGetValue(url, out var value))
            {
                var config = await base.GetOpenIdConfig(url);
                CachedDictionary.TryAdd(url, config);
                value = config;
            }

            return value;
        }
    }
}