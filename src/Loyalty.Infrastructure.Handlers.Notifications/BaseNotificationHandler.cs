using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications
{
    public abstract class BaseNotificationHandler
    {
        protected BaseNotificationHandler(ITopicClient client)
        {
            Client = client;
        }

        public ITopicClient Client { get; }
    }
}