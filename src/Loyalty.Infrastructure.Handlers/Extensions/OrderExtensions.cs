using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;

namespace Loyalty.Infrastructure.Handlers.Extensions
{
    public static class OrderExtensions
    {
        public static GetOrderByVenueIdQueryResult ToResult(this Order item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.OrderItems == null)
            {
                throw new ArgumentNullException(nameof(item.OrderItems));
            }

            var result = new GetOrderByVenueIdQueryResult
            {
                VenueId = item.VenueId,
                PlacedDate = item.PlacedDate,
                Status = item.Status,
                Comment = item.Comment,
                CustomerId = item.CreatedBy,
                Id = item.Id,
                PickUpTime = item.PickUpTime,
                OrderItems = item.OrderItems.Select(x => new GetOrderItemByVenueIdQueryResult
                {
                    ProductId = x.ProductId,
                    Amount = x.Amount,
                    ImageUrl = x.Product.ImageUri,
                    ProductName = x.Product.Name
                }).ToList()
            };

            return result;
        }

        public static List<GetOrderByVenueIdQueryResult> ToResults(this List<Order> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetOrderByVenueIdQueryResult>();
            items.ForEach(x => results.Add(x.ToResult()));

            return results;
        }
    }
}