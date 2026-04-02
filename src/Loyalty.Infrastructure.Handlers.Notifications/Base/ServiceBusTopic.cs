using System;
using Loyalty.Infrastructure.Handlers.Notifications.Interfaces;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Base
{
    public class ServiceBusTopic: IIntegrationTopicClient, IUserTopicClient
    {
        public ITopicClient Client { get; }

        public ServiceBusTopic(ITopicClient client)
        {
            Client = client;
        }
    }
}
