using System;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;

namespace Loyalty.Domain.Handlers.Extensions
{
    public static class VenueQueryExtensions
    {
        public static Venue ToSingle(this CreateVenueCommand command)
        {
            var result = new Venue
            {
                Category = command.Category,
                Description = command.Description,
                ItemId = command.Id,
                ParentId = command.ParentId,
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Location = new GeoPosition()
                {
                    Latitude = command.Latitude,
                    Longitude = command.Longitude
                }
            };

            return result;
        }
    }
}
