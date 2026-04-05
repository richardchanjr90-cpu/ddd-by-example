using System;
using System.Collections.Generic;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class ProductExtensions
    {
        public static GetProductByIdQueryResult ToResult(this Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var result = new GetProductByIdQueryResult
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                IsAvailableForOrder = item.IsAvailableForOrder,
                ImageUri = item.ImageUri?.ToString(),
                ExternalUid = item.ExternalUid,
                Description = item.Description,
                Icon = item.Icon,
                ProductGroupId = item.ProductGroupId
            };
            return result;
        }

        public static List<GetProductByIdQueryResult> ToResults(this List<Product> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetProductByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));

            return results;
        }
    }
}