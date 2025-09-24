using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Locations;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class VenueLocationExtensions
    {
        public static Location ToSingle(this CreateLocationCommand command)
        {
            var result = new Location
            {
                VenueId = command.VenueId,
                City = command.City,
                Address = command.Address,
                Latitude = command.Latitude,
                Longitude = command.Longitude
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
                Address = command.Address,
                Latitude = command.Latitude,
                Longitude = command.Longitude
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
                Address = item.Address,
                Latitude = item.Latitude,
                Longitude = item.Longitude
            };

            return result;
        }
    }
}
