using Loyalty.Core.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Extensions
{
    public static class HostConfiguratorExtensions
    {
        public static IHost Setup<T>(this HostConfigurator di, ExecutionContext context)
            where T : class
        {
            var builder = di.BuildHost(context);

            var host = builder.ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<T>();
                })
                .ConfigureData()
                .Build();

            return host;
        }
    }
}
