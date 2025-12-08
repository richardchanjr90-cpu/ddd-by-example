using Loyalty.Common.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltySettingsExtensions
    {
        public static void SetupSettings(this IServiceCollection services, IConfigurationRoot config)
        {
            services.Configure<VenueGalleryImageSettings>(
                options => config.GetSection(nameof(VenueGalleryImageSettings))
                    .Bind(options));
        }
    }
}
