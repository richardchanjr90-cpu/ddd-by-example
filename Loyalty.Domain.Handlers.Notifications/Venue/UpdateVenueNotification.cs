using System;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Venue
{
    public class UpdateVenueNotification : INotification
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public string FullDescription { get; set; }

        public string Phones { get; set; }

        public string WebSites { get; set; }

        public string WorkingHours { get; set; }

        public long? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        public string LogoUrl { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }

        public bool IsApproved { get; set; }
    }
}