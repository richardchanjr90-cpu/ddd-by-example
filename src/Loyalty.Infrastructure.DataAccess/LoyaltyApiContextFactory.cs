using System;
using System.IO;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Loyalty.Infrastructure.DataAccess
{
    public class LoyaltyApiContextFactory : IDesignTimeDbContextFactory<LoyaltyDbContext>
    {
        private static IConfigurationRoot configuration;

        public LoyaltyDbContext CreateDbContext(params string[] args)
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

            var optionsBuilder = new DbContextOptionsBuilder<LoyaltyDbContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetSection($"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}").Value,
                m => { m.EnableRetryOnFailure(); });

            return new LoyaltyDbContext(optionsBuilder.Options);
        }
    }
}