using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Base;
using MediatR;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.Notification
{
    public class NotificationHandler<T>
        : BaseNotificationHandler, INotificationHandler<T>
        where T : IIntegrationEventsNotification
    {
        private readonly ITopicClient client;

        public NotificationHandler(ITopicClient client) : base(client)
        {
            this.client = client;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}