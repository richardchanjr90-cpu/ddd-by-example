using System;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class PatchOrderAcceptanceCommand : IRequest<ICommandResult>
    {
        public long VenueId { get; set; }

        public bool Accept { get; set; }
    }
}