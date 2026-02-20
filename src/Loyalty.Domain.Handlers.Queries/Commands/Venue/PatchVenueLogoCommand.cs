using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class PatchVenueLogoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Logo { get; set; }

        public string SmallLogo { get; set; }
    }
}
