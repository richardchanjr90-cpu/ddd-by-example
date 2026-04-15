using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Infrastructure.Commands.Repository;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Outbox;
using Loyalty.Infrastructure.Outbox.Outbox;
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

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductGroupRepository, ProductGroupRepository>();
            services.AddTransient<IVenueRepository, VenueRepository>();
            services.AddTransient<IWorkerRepository, WorkerRepository>();

            services.AddTransient<IIntegrationEventService, LoggingIntegrationEventService>();
            services.AddTransient<IEventBusService, EventBusPublishingService>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContextPool<LoyaltyDbContext>(
                    options => options.UseSqlServer(
                        connectionString, x => x.EnableRetryOnFailure()));
        }
    }
}
