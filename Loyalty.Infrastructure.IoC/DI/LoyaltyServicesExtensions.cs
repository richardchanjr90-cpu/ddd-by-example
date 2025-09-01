using Loyalty.Application.Venue;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyServicesExtensions
    {
        public static void SetupAppServices(this IServiceCollection collection)
        {
            collection.AddScoped<LoyaltyVenueAppService>();
            collection.AddScoped<LoyaltyVenueDetailsAppService>();
            collection.AddScoped<LoyaltyVenueImageAppService>();
        }
    }
}
