using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications
{
    public abstract class BaseNotificationHandler
    {
        protected BaseNotificationHandler(IQueueClient client)
        {
            Client = client;
        }

        public IQueueClient Client { get; }
    }
}