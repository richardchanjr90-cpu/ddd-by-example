using System;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Infrastructure.Commands.Repository;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using Loyalty.Infrastructure.Events.DataAccess.Context.Interface;
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
            services.AddTransient<ITenantProvider, TenantTokenProvider>();

            var connectionString = config[$"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}"];
            var dapperConnection = connectionString;

            services.AddTransient(x => new SqlConnection(dapperConnection));

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductGroupRepository, ProductGroupRepository>();
            services.AddTransient<IVenueRepository, VenueRepository>();
            services.AddTransient<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IVenueAdminRepository, VenueAdminRepository>();
            services.AddTransient<ILoyaltyProgramRepository, LoyaltyProgramRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();

            services.AddTransient<IIntegrationEventService, LoggingIntegrationEventService>();
            services.AddTransient<IEventBusService, EventBusPublishingService>();

            services.AddScopedContext<ILoyaltyDbContext, LoyaltyDbContext>();
            services.AddScopedContext<ILoyaltyTenantDbContext, LoyaltyTenantDbContext>();
            services.AddScopedContext<IIntegrationEventsContext, IntegrationEventsContext>();

            var optionsBuilder = new DbContextOptionsBuilder<LoyaltyDbContext>();
            optionsBuilder.UseSqlServer(connectionString, m => { m.EnableRetryOnFailure(); });
            var options = optionsBuilder.Options;

            var optionsBuilder2 = new DbContextOptionsBuilder<IntegrationEventsContext>();
            optionsBuilder2.UseSqlServer(connectionString, m => { m.EnableRetryOnFailure(); });
            var options2 = optionsBuilder2.Options;

            services.AddTransient(x => options);
            services.AddTransient(x => options2);
        }
    }
}
