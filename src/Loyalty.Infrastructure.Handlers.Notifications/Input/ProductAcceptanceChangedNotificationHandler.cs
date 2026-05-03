using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Notifications.Input
{
    public class ProductAcceptanceChangedNotificationHandler
        : INotificationHandler<ProductAcceptanceChangedNotification>
    {
        private readonly IVenueRepository venueRepository;
        private readonly IProductRepository productRepository;

        public ProductAcceptanceChangedNotificationHandler(
            IVenueRepository venueRepository,
            IProductRepository productRepository)
        {
            this.venueRepository = venueRepository;
            this.productRepository = productRepository;
        }

        public async Task Handle(ProductAcceptanceChangedNotification notification, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByVenueAsync(notification.Id, cancellationToken);

            if (!products.Any(x => x.IsAvailableForOrder))
            {
                var venue = await venueRepository.GetAsync(notification.Id, cancellationToken);
                venue.RejectNewOrders();
                venueRepository.Update(venue);
                await venueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }
    }
}