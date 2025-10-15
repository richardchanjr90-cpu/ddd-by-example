using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Infrastructure.DataAccess;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SecurityDriven.TinyORM;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyDbExtensions
    {
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public static void SetupDb(this IServiceCollection services, IConfigurationRoot config)
        {
            //services.AddScoped<ILoyaltyDbContext, LoyaltyDbContext>();
            var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];

            services.AddScoped<DbContext>((sp) => DbContext.Create(connectionString));
            ////todo: remove for staging and prod;
            //services.AddEntityFrameworkSqlServer()
            //    .AddLogging()
            //    .AddDbContext<LoyaltyDbContext>(
            //        options => options.UseSqlServer(
            //            connectionString).UseLoggerFactory(MyLoggerFactory));
        }
    }
}
