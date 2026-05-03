using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Infrastructure.Handlers.Notifications.Base;
using Loyalty.Infrastructure.Handlers.Notifications.Interfaces;
using MediatR;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Output.Notification
{
    public class NotificationHandler<T>
        : BaseNotificationHandler, INotificationHandler<T>
        where T : IIntegrationEventsNotification
    {
        private readonly ITopicClient client;

        public NotificationHandler(IIntegrationTopicClient client) 
            : base(client.Client)
        {
            this.client = client.Client;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            var message = notification.ToMessage();
            await client.SendAsync(message);
        }
    }
}