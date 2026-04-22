using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Venues
{
    public class PatchOrderAcceptanceCommandHandler
        : IRequestHandler<PatchOrderAcceptanceCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;
        private readonly IProductRepository productRepository;

        public PatchOrderAcceptanceCommandHandler(IVenueRepository venueRepository, IProductRepository productRepository)
        {
            this.venueRepository = venueRepository;
            this.productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(PatchOrderAcceptanceCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetAsync(request.VenueId, cancellationToken);
            var products = await productRepository.GetByVenueAsync(request.VenueId, cancellationToken);

            if (request.Accept)
            {
                venue.AcceptNewOrders(products);
            }
            else
            {
                venue.RejectNewOrders();
            }

            venueRepository.Update(venue);

            var result = new CommandResult
            {
                Success = await venueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = venue.Id
            };

            return result;
        }
    }
}
