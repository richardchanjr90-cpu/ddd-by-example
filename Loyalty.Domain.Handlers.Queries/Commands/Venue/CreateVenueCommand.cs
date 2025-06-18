using System;
using Loyalty.Core.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class CreateVenueCommand : IRequest<ICommandResult>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string City { get; set; }

        public Guid? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategory Category { get; set; }
    }
}