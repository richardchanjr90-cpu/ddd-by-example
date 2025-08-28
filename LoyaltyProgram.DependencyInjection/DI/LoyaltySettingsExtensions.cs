using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Shared.Settings;
using Loyalty.Venue.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyProgram.DependencyInjection.DI
{
    public static class LoyaltySettingsExtensions
    {
        public static void SetupSettings(this IServiceCollection services, IConfigurationRoot config)
        {
            services.Configure<AuthSettings>(options => config.GetSection(nameof(AuthSettings)).Bind(options));
        }
    }
}
