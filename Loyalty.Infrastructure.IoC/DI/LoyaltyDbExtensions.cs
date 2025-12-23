using System.Data.SqlClient;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyDbExtensions
    {
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public static void SetupDb(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddTransient<ILoyaltyDbContext, LoyaltyDbContext>();
            services.AddTransient<ILoyaltyTenantDbContext, LoyaltyTenantDbContext>();
            services.AddTransient<ITenantProvider, TenantTokenProvider>();

            var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];
            var dapperConnection = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];
            services.AddScoped(x => new SqlConnection(dapperConnection));

            //todo: remove for staging and prod;
            services.AddEntityFrameworkSqlServer()
                .AddLogging()
                .AddDbContext<LoyaltyDbContext>(
                    options => options.UseSqlServer(
                            connectionString, x => x.EnableRetryOnFailure())
                        .UseLoggerFactory(MyLoggerFactory));
        }
    }
}
