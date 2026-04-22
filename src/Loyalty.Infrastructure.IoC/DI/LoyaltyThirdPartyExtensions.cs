using System;
using System.Linq;
using AutoMapper;
using Loyalty.Application.AutoMapper;
using Loyalty.Application.DomainEvents.Handlers.ProductGroup;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Infrastructure.Firebase.Handlers;
using Loyalty.Infrastructure.Handlers;
using Loyalty.Infrastructure.Handlers.Commands;
using Loyalty.Infrastructure.Handlers.Commands.Pipelines;
using Loyalty.Infrastructure.Handlers.Notifications.Base;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyThirdPartyExtensions
    {
        public static void SetupThirdParty(this IServiceCollection services)
        {
            services.AddLogging();
            services.AddMediatR(typeof(BaseHandler).Assembly);
            services.AddMediatR(typeof(BaseNotificationHandler).Assembly);
            services.AddMediatR(typeof(BaseFirebaseHandler).Assembly);
            services.AddMediatR(typeof(ProductGroupArchivedDomainEventHandler).Assembly);
            services.AddMediatR(typeof(BaseDapperHandler).Assembly);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            if (!EnvironmentExtensions.IsProd())
            {
                var serviceProvider = services.BuildServiceProvider();
                var mapper = serviceProvider.GetRequiredService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
        }
    }
}