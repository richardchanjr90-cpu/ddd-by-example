using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Infrastructure.DataAccess;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Loyalty.Infrastructure.IoC.DI
{
    public static class LoyaltyServiceBusExtensions
    {
        public static void SetupServiceBus(this IServiceCollection services, IConfigurationRoot config)
        {
            var connectionString = config[$"{nameof(ServiceBusSettings)}:{nameof(ServiceBusSettings.ConnectionString)}"];
            var queueName = config[$"{nameof(ServiceBusSettings)}:{nameof(ServiceBusSettings.BusinessActivitiesQueueName)}"];

            IQueueClient client = new QueueClient(connectionString, queueName);
            services.AddSingleton<IQueueClient>((IQueueClient)client);
        }
    }
}
