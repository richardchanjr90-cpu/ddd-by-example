using System;
using System.Collections.Generic;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.GeoPosition;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

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
                ParentId = command.ParentId,
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Location = new GeoPosition
                {
                    Latitude = command.Location.Latitude,
                    Longitude = command.Location.Longitude,
                    City = command.Location.City,
                }
            };

            return result;
        }

        public static Venue ToSingle(this UpdateVenueCommand command)
        {
            var result = new Venue
            {
                Category = command.Category,
                Description = command.Description,
                ParentId = command.ParentId,
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Location = new GeoPosition
                {
                    Latitude = command.Location.Latitude,
                    Longitude = command.Location.Longitude,
                    City = command.Location.City,
                }
            };

            return result;
        }

        public static GetVenueByIdQueryResult ToResult(this Venue item)
        {
            var result = new GetVenueByIdQueryResult
            {
                Category = item.Category,
                Description = item.Description,
                ParentId = item.ParentId,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
                Location = new GetGeoPositionQueryResult
                {
                    Latitude = item.Location.Latitude,
                    Longitude = item.Location.Longitude,
                    City = item.Location.City,
                }
            };
            return result;
        }

        public static List<GetVenueByIdQueryResult> ToResults(this List<Venue> items)
        {
            var results = new List<GetVenueByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));
            return results;
        }
    }
}
