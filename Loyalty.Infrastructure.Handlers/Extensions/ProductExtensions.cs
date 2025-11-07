using System;
using System.Collections.Generic;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class ProductExtensions
    {
        public static GetProductByIdQueryResult ToResult(this Product item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var result = new GetProductByIdQueryResult
            {
                Id = item.Id,
                Name = item.Name,
                Icon = item.Icon,
                ProductGroupId = item.ProductGroupId
            };
            return result;
        }

        public static List<GetProductByIdQueryResult> ToResults(this List<Product> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            var results = new List<GetProductByIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));

            return results;
        }

        public static Product ToEntity(this UpdateProductCommand item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var result = new Product
            {
                Id = item.Id,
                Name = item.Name,
                Icon = item.Icon,
                ProductGroupId = item.ProductGroupId
            };

            return result;
        }

        public static Product ToEntity(this CreateProductCommand item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var result = new Product
            {
                Name = item.Name,
                Icon = item.Icon,
                ProductGroupId = item.ProductGroupId
            };

            return result;
        }

        public static List<Product> ToEntities(this List<CreateProductCommand> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            var results = new List<Product>();
            items.ForEach(x => results.Add(x.ToEntity()));

            return results;
        }

        public static List<Product> ToEntities(this List<UpdateProductCommand> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            var results = new List<Product>();
            items.ForEach(x => results.Add(x.ToEntity()));

            return results;
        }
    }
}