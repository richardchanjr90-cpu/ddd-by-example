using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Shared.Enums;
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
                CategoryType = command.CategoryType,
                Description = command.Description,
                Location = new Location
                {
                    Latitude = command.Location.Latitude,
                    VenueId = command.Location.VenueId,
                    Longitude = command.Location.Longitude,
                    City = command.Location.City,
                    Id = command.Location.Id
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
                CategoryType = command.CategoryType,
                Description = command.Description,
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Location = new Location
                {
                    Latitude = command.Location.Latitude,
                    VenueId = command.Location.VenueId,
                    Longitude = command.Location.Longitude,
                    City = command.Location.City,
                    Id = command.Location.Id
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
                CategoryType = item.CategoryType,
                Description = item.Description,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
               
                Location = new GetLocationQueryResult
                {
                    Latitude = item.Location.Latitude,
                    Longitude = item.Location.Longitude,
                    City = item.Location.City,
                    VenueId = item.Location.VenueId,
                    Id = item.Location.Id
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
