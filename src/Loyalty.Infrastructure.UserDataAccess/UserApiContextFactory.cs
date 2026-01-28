using System;
using System.IO;
using Loyalty.Common.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Loyalty.Infrastructure.UserDataAccess
{
    public class UserApiContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        private static IConfigurationRoot configuration;

        public UserDbContext CreateDbContext(params string[] args)
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

            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetSection($"{nameof(DbSettings)}:{nameof(DbSettings.ConnectionString)}").Value,
                m => { m.EnableRetryOnFailure(); });

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}