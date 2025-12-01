using System;
using Loyalty.Application.Venue;
using Loyalty.Infrastructure.IoC.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyProgram.Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfigurationRoot root = null;

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    root = config
                        .SetBasePath(Environment.CurrentDirectory + "\\..\\..\\..\\..\\LoyaltyProgram")
                        .AddJsonFile("local.settings.json", true, true)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.SetupAppServices();
                    services.SetupDb(root);
                    services.SetupThirdParty();
                    services.SetupSettings(root);
                    services.SetupServiceBus(root);
                });

            var host = builder.Build();
            var service = host.Services.GetRequiredService<LoyaltyVenueAppService>();
            //var result = service.Get().GetAwaiter().GetResult();

            //Console.WriteLine(result);
        }
    }
}