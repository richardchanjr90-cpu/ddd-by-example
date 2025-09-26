using System;
using Loyalty.Common.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class UpdateVenueCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public UpdateLocationCommand Location { get; set; }

        public long? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        public string LogoUrl { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }

        public bool IsApproved { get; set; }
    }
}