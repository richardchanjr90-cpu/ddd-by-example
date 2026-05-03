using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Venues.Admin
{
    public class RejectVenueCommandHandler : 
        IRequestHandler<RejectVenuePatchCommand, ICommandResult>
    {
        private readonly IVenueAdminRepository venueRepository;

        public RejectVenueCommandHandler(IVenueAdminRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public async Task<ICommandResult> Handle(RejectVenuePatchCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetWithoutQueryFiltersAsync(request.Id, cancellationToken);

            venue.Reject();

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
