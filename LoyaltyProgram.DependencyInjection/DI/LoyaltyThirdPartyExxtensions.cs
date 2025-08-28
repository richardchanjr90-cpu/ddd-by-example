using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Loyalty.Domain.AutoMapper;
using Loyalty.Domain.Handlers;
using Loyalty.Venue.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyProgram.DependencyInjection.DI
{
    public static class LoyaltyThirdPartyExxtensions
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
