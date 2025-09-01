using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
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
