using AutoMapper;
using Loyalty.Application.AutoMapper;
using Loyalty.Infrastructure.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyThirdPartyExtensions
    {
        public static void SetupThirdParty(this IServiceCollection services)
        {
            services.AddMediatR(typeof(BaseHandler).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));

            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
