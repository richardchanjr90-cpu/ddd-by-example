using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyProgram.Extensions
{
    public static class HostExtensions
    {
        public static T StartService<T>(this IHost host)
        {
            var mapper = host.Services.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            host.Start();
            var service = host.Services.GetService<T>();
            return service;
        }
    }
}
