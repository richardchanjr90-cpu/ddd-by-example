using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Domain.Handlers.Queries.Queries.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Orders
{
    public class GetOrdersByUserIdQueryHandler
        : BaseDapperHandler,  IRequestHandler<GetOrdersByUserIdQuery, GetOrdersByUserIdQueryResult>
    {
        private const string SelectQuery = @"SELECT 
                                               o.[Id]
                                              ,o.[VenueId]
                                              ,o.[PlacedDate]
                                              ,o.[CreatedBy] as CustomerId
                                              ,o.[PickUpTime]
                                              ,o.[Comment]
                                              ,o.[Status]
                                              ,oir.[Id]
                                              ,oir.[Amount]
                                              ,oir.[ProductId]
                                              ,pr.[Price]
                                              ,pr.[Name] as [ProductName]
                                              ,pr.ImageUri as ImageUrl
                                          FROM [loyalty].[Order] o JOIN [loyalty].OrderItem oir ON o.Id = oir.OrderId
                                          JOIN [loyalty].Product pr ON pr.Id = oir.ProductId
                                          WHERE o.CustomerId = @userId AND o.VenueId = @venueId";

        public GetOrdersByUserIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetOrdersByUserIdQueryResult> Handle(
            GetOrdersByUserIdQuery request, 
            CancellationToken cancellationToken)
        {
            var venueId = request.VenueId;
            var userId = request.UserId;

            using (Connection)
            {
                Connection.Open();

                var orderDictionary = new Dictionary<long, GetOrderByUserIdQueryResult>();

                var rows = Connection.Query<
                        GetOrderByUserIdQueryResult,
                        GetOrderItemByVenueIdQueryResult,
                        GetOrderByUserIdQueryResult>(
                        SelectQuery,
                        (order, orderItem) =>
                        {
                            if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                            {
                                orderEntry = order;
                                orderEntry.OrderItems = new List<GetOrderItemByVenueIdQueryResult>();
                                orderDictionary.Add(order.Id, order);
                            }

                            orderEntry.OrderItems.Add(orderItem);
                            return orderEntry;
                        }, new
                        {
                            venueId,
                            userId
                        },
                        splitOn: "Id")
                    .Distinct()
                    .ToList();

                return Task.FromResult(new GetOrdersByUserIdQueryResult()
                {
                    Orders = rows ?? new List<GetOrderByUserIdQueryResult>()
                });
            }
        }
    }
}
