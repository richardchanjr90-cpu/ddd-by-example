using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Venue.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyProgram.DependencyInjection.DI
{
    public static class LoyaltyDbExtensions
    {
        public static void SetupDb(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddScoped<ILoyaltyDbContext, LoyaltyDbContext>();

            var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<LoyaltyDbContext>(
                    options => options.UseSqlServer(
                        connectionString));
        }
    }
}
