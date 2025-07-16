using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Loyalty.Core.Auth.Validation
{
    public class TokenValidator
    {
        private readonly AuthSettings settings;

        public TokenValidator(AuthSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected virtual string BuildUrl => string.Format(
            settings.DiscoveryUri,
            settings.Tenant,
            settings.Tenant,
            settings.AuthPolicy);

        protected virtual async Task<OpenIdConnectConfiguration> GetOpenIdConfig(string url)
        {
            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                url,
                new OpenIdConnectConfigurationRetriever());

            return await configManager.GetConfigurationAsync();
        }

        protected virtual TokenValidationParameters ConfigureValidation(OpenIdConnectConfiguration config)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,

                IssuerSigningKeys = config.SigningKeys,
                ValidAudience = settings.Audience,
                ValidIssuer = config.Issuer
            };

            return validationParameters;
        }

        public virtual async Task<JwtSecurityToken> GetToken(string token)
        {
            var config = await GetOpenIdConfig(BuildUrl);
            var parameters = ConfigureValidation(config);

            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ValidateToken(token, parameters, out var jwt);

            Thread.CurrentPrincipal = result;

            return jwt as JwtSecurityToken;
        }
    }
}
