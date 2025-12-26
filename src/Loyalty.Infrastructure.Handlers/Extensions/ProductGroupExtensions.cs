using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class ProductGroupExtensions
    {
        public static GetProductGroupByIdQueryResult ToResult(this ProductGroup item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetProductGroupByIdQueryResult
            {
                Id = item.Id,
                Icon = item.Icon,
                Name = item.Name,
                VenueId = item.VenueId,
                Products = item.Products?.ToList().ToResults()
            };

            return result;
        }

        public static List<GetProductGroupByIdQueryResult> ToResults(this List<ProductGroup> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetProductGroupByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));

            return results;
        }
    }
}