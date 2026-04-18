using Loyalty.Common.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltySettingsExtensions
    {
        public static void SetupSettings(this IServiceCollection services, IConfigurationRoot config)
        {
            services.Configure<ImageSettings>(
                options => config.GetSection(nameof(ImageSettings))
                    .Bind(options));

            services.Configure<ImageStorageSettings>(
                options => config.GetSection(nameof(ImageStorageSettings))
                    .Bind(options));

            services.Configure<GoogleAuthSettings>(
                options => config.GetSection(nameof(GoogleAuthSettings))
                    .Bind(options));

            services.Configure<VenueSettings>(
                options => config.GetSection(nameof(VenueSettings))
                    .Bind(options));

            services.Configure<SmsSettings>(
                options => config.GetSection(nameof(SmsSettings))
                    .Bind(options));

            services.Configure<EmailSettings>(
                options => config.GetSection(nameof(EmailSettings))
                    .Bind(options));
        }
    }
}
