using System;
using Loyalty.Infrastructure.IoC.DI;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Loyalty.Infrastructure.IoC.Startup))]

namespace Loyalty.Infrastructure.IoC
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.SetupAppServices();
            builder.Services.SetupDb(config);
            builder.Services.SetupThirdParty();
            builder.Services.SetupServiceBus(config);
            builder.Services.SetupSettings(config);
        }
    }
}
