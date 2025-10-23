using System;
using System.Collections.Generic;
using Loyalty.Core.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Location;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class CreateVenueCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public CreateLocationCommand Location { get; set; }

        public long? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        public string LogoUrl { get; set; }

        public CreateVenueDetailsCommand Details { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }

        public bool IsApproved { get; set; }
    }
}
