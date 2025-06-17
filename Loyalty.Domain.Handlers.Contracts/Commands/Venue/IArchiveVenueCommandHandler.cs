using System;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Venue
{
    public interface IArchiveVenueCommandHandler : IRequestHandler<ArchiveVenueCommand, ICommandResult>
    {
    }
}
