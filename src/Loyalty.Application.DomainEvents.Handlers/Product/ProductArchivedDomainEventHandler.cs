using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductArchivedDomainEventHandler :
        INotificationHandler<ProductArchivedDomainEvent>
    {
        private readonly IMediator mediator;

        public ProductArchivedDomainEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ProductArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await mediator.Publish(
                new ArchiveProductNotification
                {
                    Id = domainEvent.Product.Id,
                    IsArchived = true,
                }, cancellationToken);
        }
    }
}