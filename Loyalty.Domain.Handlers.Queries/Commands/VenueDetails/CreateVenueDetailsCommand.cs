using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.VenueDetails
{
    public class CreateVenueDetailsCommand : IRequest<ICommandResult>
    {
        public long VenueId { get; set; }

        public string FullDescription { get; set; }

        public List<string> Phones { get; set; } = new List<string>();

        public List<string> WebSites { get; set; } = new List<string>();

        public List<string> WorkingHours { get; set; } = new List<string>();
    }
}