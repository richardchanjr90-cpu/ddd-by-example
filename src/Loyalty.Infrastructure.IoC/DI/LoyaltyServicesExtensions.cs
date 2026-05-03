using Loyalty.Application.Venue;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyServicesExtensions
    {
        public static void SetupAppServices(this IServiceCollection collection)
        {
            collection.AddScoped<LoyaltyVenueAppService>();
            collection.AddScoped<LoyaltyVenueImageAppService>();
            collection.AddScoped<PurchaseAppService>();
            collection.AddScoped<LoyaltyProductGroupAppService>();
            collection.AddScoped<LoyaltyProgramAppService>();
            collection.AddScoped<ProductGroupAppService>();
            collection.AddScoped<ProductAppService>();
            collection.AddScoped<WorkerAppService>();
            collection.AddScoped<LoyaltySignupAppService>();
            collection.AddScoped<CodeAppService>();
            collection.AddScoped<ClientInfoAppService>();
            collection.AddScoped<SignupAppService>();
            collection.AddScoped<OrderAppService>();
            collection.AddScoped<RateUserAppService>();
        }
    }
}