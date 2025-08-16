using System;
using System.Collections.Generic;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.Handlers.Extensions
{
    public static class VenueExtensions
    {
        public static Venue ToSingle(this CreateVenueCommand command)
        {
            var result = new Venue
            {
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Category = command.Category,
                Description = command.Description,
                Location = new Location
                {
                    Latitude = command.Location.Latitude,
                    Longitude = command.Location.Longitude,
                    City = command.Location.City,
                },
                VenueDetails = command.Details?.ToSingle(),
                IsPublished = false,
                LogoUrl = command.LogoUrl
            };

            return result;
        }

        public static Venue ToSingle(this UpdateVenueCommand command)
        {
            var result = new Venue
            {
                Id = command.Id,
                Category = command.Category,
                Description = command.Description,
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Location = new Location
                {
                    Latitude = command.Location.Latitude,
                    Longitude = command.Location.Longitude,
                    City = command.Location.City,
                },
                LogoUrl = command.LogoUrl,
                VenueDetails = command.Details?.ToSingle()
            };

            return result;
        }

        public static GetVenueByIdQueryResult ToResult(this Venue item)
        {
            var result = new GetVenueByIdQueryResult
            {
                Id = item.Id,
                Category = item.Category,
                Description = item.Description,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
               
                Location = new GetLocationQueryResult
                {
                    Latitude = item.Location.Latitude,
                    Longitude = item.Location.Longitude,
                    City = item.Location.City,
                },

                IsPublished = item.IsPublished,
                LogoUrl = item.LogoUrl
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
