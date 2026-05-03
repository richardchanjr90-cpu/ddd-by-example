using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Venues
{
    public class ArchiveVenueCommandHandler :
        IRequestHandler<ArchiveVenueCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;

        public ArchiveVenueCommandHandler(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public async Task<ICommandResult> Handle(ArchiveVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetAsync(request.Id, cancellationToken);

            if (venue != null)
            {
                venue.Archive();
                venueRepository.Update(venue);
            }

            var result = new CommandResult
            {
                Success = await venueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = venue?.Id
            };

            return result;
        }
    }
}
