using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.ServiceBus.Handlers.Queries.Venue;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class VenueExtensions
    {
        public static Venue ToSingle(this CreateVenueCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var result = new Venue
            {
                Name = command.Name,
                Type = command.Type,
                ParentId = command.ParentId,
                OwnerId = command.OwnerId,
                CategoryType = command.CategoryType,
                Description = command.Description,
                Location = command.Location?.ToSingle(),
                LogoUrl = command.LogoUrl,
                FullDescription = command.FullDescription,
                WebSites = command.WebSites.ToCommaSeparatedStringOrNull(),
                WorkingHours = command.WorkingHours.ToCommaSeparatedStringOrNull(),
                Phones = command.Phones.ToCommaSeparatedStringOrNull(),
                IsArchived = command.IsArchived,
                IsPublished = command.IsPublished,
                IsApproved = command.IsApproved
            };

            return result;
        }

        public static CreateVenueNotification ToNotification(this Venue item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new CreateVenueNotification
            {
                Id = item.Id,
                CategoryType = item.CategoryType,
                Description = item.Description,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
                //Location = item.Location?.ToResult(),
                IsPublished = item.IsPublished,
                LogoUrl = item.LogoUrl,
                Phones = item.Phones.SplitByCommaAndUnwrap(),
                FullDescription = item.FullDescription,
                WebSites = item.WebSites.SplitByCommaAndUnwrap(),
                WorkingHours = item.WorkingHours.SplitByCommaAndUnwrap(),
                IsArchived = item.IsArchived,
                IsApproved = item.IsApproved,
                ParentId = item.ParentId
            };

            return result;
        }

        public static Venue ToSingle(this UpdateVenueCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var result = new Venue
            {
                Id = command.Id,
                CategoryType = command.CategoryType,
                Description = command.Description,
                ParentId = command.ParentId,
                Name = command.Name,
                Type = command.Type,
                OwnerId = command.OwnerId,
                Location = command.Location?.ToSingle(),
                FullDescription = command.FullDescription,
                WebSites = command.WebSites.ToCommaSeparatedStringOrNull(),
                WorkingHours = command.WorkingHours.ToCommaSeparatedStringOrNull(),
                Phones = command.Phones.ToCommaSeparatedStringOrNull(),
                LogoUrl = command.LogoUrl,
                IsArchived = command.IsArchived,
                IsPublished = command.IsPublished,
                IsApproved = command.IsApproved
            };

            return result;
        }

        public static GetVenueByIdQueryResult ToResult(this Venue item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetVenueByIdQueryResult
            {
                Id = item.Id,
                CategoryType = item.CategoryType,
                Description = item.Description,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
                Location = item.Location?.ToResult(),
                IsPublished = item.IsPublished,
                LogoUrl = item.LogoUrl,
                Phones = item.Phones.SplitByCommaAndUnwrap(),
                FullDescription = item.FullDescription,
                WebSites = item.WebSites.SplitByCommaAndUnwrap(),
                WorkingHours = item.WorkingHours.SplitByCommaAndUnwrap(),
                IsArchived = item.IsArchived,
                IsApproved = item.IsApproved,
                ParentId = item.ParentId
            };
            return result;
        }

        public static List<GetVenueByIdQueryResult> ToResults(this List<Venue> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetVenueByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));
            return results;
        }
    }
}
