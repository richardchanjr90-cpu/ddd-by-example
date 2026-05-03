using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Interfaces
{
    public interface IIntegrationTopicClient : IServiceBusTopic
    {
    }
}
