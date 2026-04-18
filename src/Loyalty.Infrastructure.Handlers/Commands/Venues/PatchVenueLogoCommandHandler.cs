using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class PatchVenueLogoCommandHandler : IRequestHandler<PatchVenueLogoCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;

        public PatchVenueLogoCommandHandler(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public async Task<ICommandResult> Handle(PatchVenueLogoCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetAsync(request.Id, cancellationToken);

            venue.ChangeLogo(request.Logo);
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
