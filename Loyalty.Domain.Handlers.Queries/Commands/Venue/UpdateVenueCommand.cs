using System;
using Loyalty.Core.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class UpdateVenueCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public GetLocationQueryResult Location { get; set; }

        public VenueType Type { get; set; }

        public VenueCategory Category { get; set; }

        public string LogoUrl { get; set; }

        public UpdateVenueDetailsCommand Details { get; set; }
    }
}