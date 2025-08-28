using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.VenueDetails
{
    public interface ICreateVenueDetailsCommandHandler : IRequestHandler<CreateVenueDetailsCommand, ICommandResult>
    {
    }
}
