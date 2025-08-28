using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Venue.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyProgram.DependencyInjection.DI
{
    public static class LoyaltyServicesExtensions
    {
        public static void SetupAppServices(this IServiceCollection collection)
        {
            collection.AddScoped<LoyaltyVenueAppService>();
            collection.AddScoped<LoyaltyVenueDetailsAppService>();
        }
    }
}
