using System;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.Events.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Loyalty.Infrastructure.Events.DataAccess
{
    public class IntegrationApiContextFactory : IDesignTimeDbContextFactory<IntegrationEventsContext>
    {
        private static IConfigurationRoot configuration;

        public IntegrationEventsContext CreateDbContext(params string[] args)
        {
            var defaultPath =
                $"{Environment.CurrentDirectory}\\..\\LoyaltyProgram";

            if (args.Length == 1)
            {
                defaultPath = args[0];
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(defaultPath)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables();

            configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventsContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetSection($"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}").Value,
                m => { m.EnableRetryOnFailure(); });

            return new IntegrationEventsContext(optionsBuilder.Options);
        }
    }
}