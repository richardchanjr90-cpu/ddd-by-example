using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.VenueCategories;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.Handlers.Extensions
{
    public static class VenueCategoryExtensions
    {
        public static VenueCategory ToSingle(this CreateVenueCategoryCommand command)
        {
            var result = new VenueCategory
            {
                VenueId = command.Id,
                CategoryType = command.CategoryType,
            };

            return result;
        }

        public static VenueCategory ToSingle(this UpdateVenueCategoryCommand command)
        {
            var result = new VenueCategory
            {
                Id = command.Id,
                CategoryType = command.CategoryType,
            };

            return result;
        }

        public static List<VenueCategory> ToResults(this List<CreateVenueCategoryCommand> items)
        {
            var results = new List<VenueCategory>();
            items.ForEach(x => results.Add(x.ToSingle()));
            return results;
        }

        public static List<VenueCategory> ToResults(this List<UpdateVenueCategoryCommand> items)
        {
            var results = new List<VenueCategory>();
            items.ForEach(x => results.Add(x.ToSingle()));
            return results;
        }
    }
}
