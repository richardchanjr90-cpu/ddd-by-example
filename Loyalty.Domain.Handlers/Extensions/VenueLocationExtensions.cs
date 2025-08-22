using System;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;

namespace Loyalty.Domain.Handlers.Extensions
{
    public static class VenueLocationExtensions
    {
        public static Location ToSingle(this CreateLocationCommand command)
        {
            var result = new Location
            {
                VenueId = command.VenueId,
                City = command.City,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
            };

            return result;
        }

        public static Location ToSingle(this UpdateLocationCommand command)
        {
            var result = new Location
            {
                Id = command.Id,
                VenueId = command.VenueId,
                City = command.City,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
            };

            return result;
        }

        public static GetLocationQueryResult ToResult(this Location item)
        {
            var result = new GetLocationQueryResult
            {
                Id = item.Id,
                VenueId = item.VenueId,
                City = item.City,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
            };

            return result;
        }
    }
}
