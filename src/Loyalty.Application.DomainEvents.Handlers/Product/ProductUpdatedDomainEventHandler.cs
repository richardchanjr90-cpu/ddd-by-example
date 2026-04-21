using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Products;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Product
{
    public class ProductUpdatedDomainEventHandler :
        INotificationHandler<ProductUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public ProductUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(ProductUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var product = domainEvent.Product;

            await eventBusService.PersistEventAsync(
                new UpdateProductNotification
                {
                    Price = product.Price,
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name
                });
        }
    }
}