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
    public class GetOrderByIdQueryHandler
        : BaseDapperHandler,  IRequestHandler<GetOrderByIdQuery, GetOrderByVenueIdQueryResult>
    {
        private const string SelectQuery = @"SELECT 
                                               o.[Id]
                                              ,o.[VenueId]
                                              ,o.[PlacedDate]
                                              ,o.[CreatedBy] as CustomerId
                                              ,o.[PickUpTime]
                                              ,o.[Comment]
                                              ,o.[Status]
                                              ,o.[Rate]
                                              ,oir.[Id]
                                              ,oir.[Amount]
                                              ,oir.[ProductId]
                                              ,pr.[Price]
                                              ,pr.[Name] as [ProductName]
                                              ,pr.ImageUri as ImageUrl
                                          FROM [loyalty].[Order] o JOIN [loyalty].OrderItem oir ON o.Id = oir.OrderId
                                          JOIN [loyalty].Product pr ON pr.Id = oir.ProductId
                                          WHERE o.Id = @id";

        public GetOrderByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetOrderByVenueIdQueryResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            using (Connection)
            {
                Connection.Open();

                var orderDictionary = new Dictionary<long, GetOrderByVenueIdQueryResult>();

                var rows = Connection.Query<
                        GetOrderByVenueIdQueryResult,
                        GetOrderItemByVenueIdQueryResult,
                        GetOrderByVenueIdQueryResult>(
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
                            id
                        },
                        splitOn: "Id")
                    .Distinct()
                    .SingleOrDefault();

                return Task.FromResult(rows);
            }
        }
    }
}
