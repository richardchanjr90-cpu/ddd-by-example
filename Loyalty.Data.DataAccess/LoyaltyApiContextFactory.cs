using System;
using System.Diagnostics;
using System.IO;
using Loyalty.Core.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Loyalty.Data.DataAccess
{
    public class LoyaltyApiContextFactory : IDesignTimeDbContextFactory<LoyaltyDbContext>
    {
        private static IConfigurationRoot configuration;

        public LoyaltyDbContext CreateDbContext(params string[] args)
        {
            var defaultPath =
                $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}LoyaltyProgram";

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
