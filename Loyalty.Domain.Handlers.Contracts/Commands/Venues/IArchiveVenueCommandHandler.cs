using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Venues
{
    public interface IArchiveVenueCommandHandler : IRequestHandler<ArchiveVenueCommand, ICommandResult>
    {
    }
}