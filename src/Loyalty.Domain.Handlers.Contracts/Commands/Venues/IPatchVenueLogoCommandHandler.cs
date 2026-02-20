using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Venues
{
    public interface IPatchVenueLogoCommandHandler : IRequestHandler<PatchVenueLogoCommand, ICommandResult>
    {
    }
}
