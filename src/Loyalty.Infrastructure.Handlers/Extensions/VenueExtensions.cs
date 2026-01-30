using System;
using System.Collections.Generic;
using System.Text.Json;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.ValueObject;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Location;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class VenueExtensions
    {
        public static Venue ToSingle(this CreateVenueCommand command, string userId)
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
                OwnerId = userId,
                CategoryType = command.CategoryType,
                Description = command.Description,
                City = command.Location?.City,
                Address = command.Location?.Address,
                Latitude = command.Location?.Latitude ?? 0,
                Longitude = command.Location?.Longitude ?? 0,
                FullDescription = command.FullDescription,
                WebSites = command.WebSites.ToCommaSeparatedStringOrNull(),
                WorkingHours = JsonSerializer.Serialize(command.WorkingHours),
                Phones = command.Phones.ToCommaSeparatedStringOrNull(),
                IsPublished = command.IsPublished,
                SocialNetworks = new SocialNetworks
                {
                    Facebook = command?.SocialNetworks?.Facebook,
                    Instagram = command?.SocialNetworks?.Instagram,
                    Vkontakte = command?.SocialNetworks?.Vkontakte,
                }
            };

            return result;
        }

        public static CreateVenueNotification ToVenueNotification(this Venue item)
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
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                City = item.City,
                Address = item.Address,
                IsPublished = item.IsPublished,
                LogoUrl = item.LogoUrl,
                Phones = item.Phones,
                FullDescription = item.FullDescription,
                WebSites = item.WebSites,
                WorkingHours = item.WorkingHours,
                IsArchived = item.IsArchived,
                IsApproved = item.IsApproved,
                ParentId = item.ParentId,
            };

            if (item.SocialNetworks != null)
            {
                result.SocialNetworks = JsonSerializer.Serialize(item.SocialNetworks);
            }

            return result;
        }

        public static UpdateVenueNotification ToUpdateNotification(this Venue item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new UpdateVenueNotification
            {
                Id = item.Id,
                CategoryType = item.CategoryType,
                Description = item.Description,
                Name = item.Name,
                Type = item.Type,
                OwnerId = item.OwnerId,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                City = item.City,
                Address = item.Address,
                IsPublished = item.IsPublished,
                LogoUrl = item.LogoUrl,
                Phones = item.Phones,
                FullDescription = item.FullDescription,
                WebSites = item.WebSites,
                WorkingHours = item.WorkingHours,
                IsArchived = item.IsArchived,
                IsApproved = item.IsApproved,
                ParentId = item.ParentId,
            };

            if (item.SocialNetworks != null)
            {
                result.SocialNetworks = JsonSerializer.Serialize(item.SocialNetworks);
            }

            return result;
        }

        public static ArchiveVenueNotification ToArchiveNotification(this Venue item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new ArchiveVenueNotification
            {
                Id = item.Id,
                OwnerId = item.OwnerId
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
                City = command.Location?.City,
                Address = command.Location?.Address,
                Latitude = command.Location?.Latitude ?? 0,
                Longitude = command.Location?.Longitude ?? 0,
                FullDescription = command.FullDescription,
                WebSites = command.WebSites.ToCommaSeparatedStringOrNull(),
                WorkingHours = JsonSerializer.Serialize(command.WorkingHours),
                Phones = command.Phones.ToCommaSeparatedStringOrNull(),
                IsPublished = command.IsPublished,
                SocialNetworks = new SocialNetworks
                {
                    Facebook = command?.SocialNetworks?.Facebook,
                    Instagram = command?.SocialNetworks?.Instagram,
                    Vkontakte = command?.SocialNetworks?.Vkontakte,
                }
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
                Images = item.Images.SplitByCommaAndUnwrap(),
                Location = item.ToLocation(),
                IsPublished = item.IsPublished,
                LogoUrl = item.LogoUrl,
                Phones = item.Phones.SplitByCommaAndUnwrap(),
                FullDescription = item.FullDescription,
                WebSites = item.WebSites.SplitByCommaAndUnwrap(),
                WorkingHours = JsonSerializer.Deserialize<List<GetVenueWorkingHoursQueryResult>>(item.WorkingHours),
                IsArchived = item.IsArchived,
                IsApproved = item.IsApproved,
                ParentId = item.ParentId,
                SocialNetworks = new GetSocialNetworksResult()
                {
                    Facebook = item?.SocialNetworks?.Facebook,
                    Instagram = item?.SocialNetworks?.Instagram,
                    Vkontakte = item?.SocialNetworks?.Vkontakte,
                }
            };
            return result;
        }

        public static GetLocationQueryResult ToLocation(this Venue item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetLocationQueryResult
            {
                City = item.City,
                Address = item.Address,
                Latitude = item.Latitude,
                Longitude = item.Longitude
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