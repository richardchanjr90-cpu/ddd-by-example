using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class PatchVenueLogoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Logo { get; set; }

        public string SmallLogo { get; set; }
    }
}
