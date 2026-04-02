using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Base;
using Loyalty.Infrastructure.Handlers.Notifications.Base;
using Loyalty.Infrastructure.Handlers.Notifications.Interfaces;
using MediatR;
using Microsoft.Azure.ServiceBus;

namespace Loyalty.Infrastructure.Handlers.Notifications.Notifications.Notification
{
    public class UserNotificationHandler<T>
        : BaseNotificationHandler, INotificationHandler<T>
        where T : IUserEventsNotification
    {
        private readonly ITopicClient client;

        public UserNotificationHandler(IUserTopicClient client) 
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