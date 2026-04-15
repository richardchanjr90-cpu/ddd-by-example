using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class PatchOrderAcceptanceCommandHandler
        : IRequestHandler<PatchOrderAcceptanceCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;

        public PatchOrderAcceptanceCommandHandler(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public async Task<ICommandResult> Handle(PatchOrderAcceptanceCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetAsync(request.VenueId, cancellationToken);

            if (request.Accept)
            {
                venue.AcceptNewOrders();
            }
            else
            {
                venue.RejectNewOrders();
            }

            var result = new CommandResult
            {
                Success = await venueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = venue.Id
            };

            return result;
        }
    }
}
