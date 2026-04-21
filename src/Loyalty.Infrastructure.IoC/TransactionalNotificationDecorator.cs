using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Loyalty.Infrastructure.IoC
{
    public class TransactionalNotificationDecorator<T> : INotificationHandler<T>
        where T : class, INotification
    {
        private readonly INotificationHandler<T> handler;

        public TransactionalNotificationDecorator(INotificationHandler<T> handler)
        {
            this.handler = handler;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            await handler.Handle(notification, cancellationToken);
        }
    }
}
