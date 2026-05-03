using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.Handlers.Notifications.Base;
using Loyalty.Infrastructure.Handlers.Notifications.Interfaces;
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

            var clientTopicName =
                config[$"{nameof(ServiceBusSettings)}:{nameof(ServiceBusSettings.BusinessActivitiesQueueName)}"];

            var userTopicName =
                config[$"{nameof(ServiceBusSettings)}:{nameof(ServiceBusSettings.UserTopicName)}"];

            var clientTopic = new TopicClient(connectionString, clientTopicName);
            var userClient = new TopicClient(connectionString, userTopicName);

            services.AddSingleton<IUserTopicClient>(new ServiceBusTopic(userClient));
            services.AddSingleton<IIntegrationTopicClient>(new ServiceBusTopic(clientTopic));
        }
    }
}