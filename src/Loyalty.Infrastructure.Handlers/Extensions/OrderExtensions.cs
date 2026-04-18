using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Shared.Contracts.Enums;

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
                Status = (OrderStatus)item.Status.Id,
                Comment = item.Comment,
                CustomerId = item.CreatedBy,
                Id = item.Id,
                PickUpTime = item.PickUpTime,
                OrderItems = item.OrderItems.Select(x => new GetOrderItemByVenueIdQueryResult
                {
                    ProductId = x.ProductId,
                    Amount = x.Amount,
                    Price = x.Product.Price,
                    ImageUrl = x.Product.ImageUri,
                    ProductName = x.Product.Name
                }).ToList()
            };

            return result;
        }

        public static GetOrderByUserIdQueryResult ToUserResult(this Order item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.OrderItems == null)
            {
                throw new ArgumentNullException(nameof(item.OrderItems));
            }

            var result = new GetOrderByUserIdQueryResult
            {
                PlacedDate = item.PlacedDate,
                Status = (OrderStatus)item.Status.Id,
                Comment = item.Comment,
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

        public static List<GetOrderByUserIdQueryResult> ToUserResults(this List<Order> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var results = new List<GetOrderByUserIdQueryResult>();
            items.ForEach(x => results.Add(x.ToUserResult()));

            return results;
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