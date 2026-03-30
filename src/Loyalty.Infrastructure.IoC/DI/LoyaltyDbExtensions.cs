using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Infrastructure.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyDbExtensions
    {
        public static void SetupDb(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddTransient<ILoyaltyDbContext, LoyaltyDbContext>();
            services.AddTransient<ILoyaltyTenantDbContext, LoyaltyTenantDbContext>();
            services.AddTransient<ITenantProvider, TenantTokenProvider>();

            var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];
            var dapperConnection = connectionString;

            services.AddTransient(x => new SqlConnection(dapperConnection));

            services.AddEntityFrameworkSqlServer()
                .AddDbContextPool<LoyaltyDbContext>(
                    options => options.UseSqlServer(
                        connectionString, x => x.EnableRetryOnFailure()));
        }
    }
}
