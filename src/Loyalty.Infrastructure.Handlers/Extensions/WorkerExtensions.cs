using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Queries.Workers;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class WorkerExtensions
    {
        public static GetWorkerByIdQueryResult ToResult(this Worker item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetWorkerByIdQueryResult
            {
                Id = item.Id,
                VenueIds = item.Venues.Select(x => x.VenueId).ToList(),
                Name = item.Name,
                Email = item.Email,
                LastName = item.LastName,
                Phone = item.Phone,
                PhotoUri = item.PhotoUri,
                Role = item.Venues.Select(x => x.Role).First(),
                WorkerId = item.WorkerId,
                PositionName = item.PositionName
            };
            return result;
        }

        public static GetInviteByPhoneQueryResult ToInvite(this Worker item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetInviteByPhoneQueryResult
            {
                Id = item.Id,
                VenueIds = item?.Venues.Select(x => x.VenueId).ToList(),
                Name = item.Name,
                Phone = item.Phone,
                PositionName = item.PositionName,
                Role = item.Venues.Select(x => x.Role).First(),
            };
            return result;
        }

        public static List<GetWorkerByIdQueryResult> ToResults(this List<Worker> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetWorkerByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));

            return results;
        }
    }
}