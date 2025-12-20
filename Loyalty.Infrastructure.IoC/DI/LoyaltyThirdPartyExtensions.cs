using AutoMapper;
using Loyalty.Application.AutoMapper;
using Loyalty.Infrastructure.Handlers;
using Loyalty.Infrastructure.Handlers.Notifications;
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

            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}