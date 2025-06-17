using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Venue
{
    public interface IUpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand, ICommandResult>
    {
    }
}
