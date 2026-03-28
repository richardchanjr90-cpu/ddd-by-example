using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;

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
                Venues = item.Venues.Select(x => new GetVenueWorkerResult()
                {
                    PositionName = x.PositionName,
                    VenueId = x.VenueId,
                    Role = x.Role
                }).ToList(),
                Name = item.Name,
                Email = item.Email,
                LastName = item.LastName,
                Phone = item.Phone,
                PhotoUri = item.PhotoUri,
                WorkerId = item.WorkerId,
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
                Venues = item.Venues.Select(x => new GetVenueWorkerResult()
                {
                    PositionName = x.PositionName,
                    VenueId = x.VenueId,
                    Role = x.Role
                }).ToList(),
                Name = item.Name,
                Phone = item.Phone,
            };
            return result;
        }

        public static GetInviteByEmailQueryResult ToWorkerResult(this Worker item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetInviteByEmailQueryResult
            {
                Id = item.Id,
                Venues = item.Venues.Select(x => new GetVenueWorkerResult()
                {
                    PositionName = x.PositionName,
                    VenueId = x.VenueId,
                    Role = x.Role
                }).ToList(),
                Name = item.Name,
                Phone = item.Phone,
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