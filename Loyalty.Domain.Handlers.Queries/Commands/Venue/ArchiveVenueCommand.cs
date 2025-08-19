using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class ArchiveVenueCommand : IRequest<ICommandResult>
    {
        public Guid OwnerId { get; set; }

        public long Id { get; set; }
    }
}
