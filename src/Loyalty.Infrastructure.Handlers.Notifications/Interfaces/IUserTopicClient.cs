using System;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Interfaces
{
    public interface IUserTopicClient : IServiceBusTopic
    {
    }
}
