using System;
using System.Reflection;
using AutoMapper;
using Loyalty.Application.AutoMapper;
using Loyalty.Infrastructure.Handlers;
using Loyalty.Infrastructure.Handlers.Notifications;
using Loyalty.Infrastructure.Handlers.Pipelines;
using MediatR;
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
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandBehavior<,>));

            if (Environment.GetEnvironmentVariable("FUNCTION_ENV") != "stage")
            {
                var serviceProvider = services.BuildServiceProvider();
                var mapper = serviceProvider.GetRequiredService<IMapper>();

                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
        }
    }
}