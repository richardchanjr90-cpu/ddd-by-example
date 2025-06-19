using System;
using AutoMapper;
using Loyalty.Data.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyProgram.Extensions
{
    public static class HostExtensions
    {
        public static T StartService<T>(this IHost host)
        {
            DataMigrator.MigrateData();
            var mapper = host.Services.GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            host.Start();
            var service = host.Services.GetService<T>();
            return service;
        }
    }
}
