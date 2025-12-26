using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class PatchVenueImagesCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public List<string> Images { get; set; } = new List<string>();
    }
}
