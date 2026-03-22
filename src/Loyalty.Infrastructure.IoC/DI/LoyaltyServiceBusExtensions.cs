using Loyalty.Common.Shared.Settings;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyServiceBusExtensions
    {
        public static void SetupServiceBus(this IServiceCollection services, IConfigurationRoot config)
        {
            var connectionString =
                config[$"{nameof(ServiceBusSettings)}:{nameof(ServiceBusSettings.ConnectionString)}"];
            var queueName =
                config[$"{nameof(ServiceBusSettings)}:{nameof(ServiceBusSettings.BusinessActivitiesQueueName)}"];

            var client = new TopicClient(connectionString, queueName);
            services.AddSingleton<ITopicClient>(client);
        }
    }
}