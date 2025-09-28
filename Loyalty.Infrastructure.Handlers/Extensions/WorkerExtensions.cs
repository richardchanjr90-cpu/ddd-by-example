using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
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
                Name = item.Name,
                Email = item.Email,
                LastName = item.LastName,
                Phone = item.Phone,
                PhotoUri = item.PhotoUri,
                Role = item.Role,
                WorkerId = item.WorkerId,
                PositionName = item.PositionName
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
