using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersByUserIdQueryHandler
        : BaseDapperHandler, IRequestHandler<GetWorkersByUserIdQuery, GetWorkersByUserIdQueryResult>
    {
        private const string Query = @"SELECT w.*, vw.[Role] as [Role], vw.VenueId as VenueId, vw.PositionName as PositionName FROM loyalty.Worker w 
                    join loyalty.VenueWorker vw on w.Id = vw.WorkerId
                    WHERE 
                    vw.VenueId in @ids AND
                    (w.WorkerId != @userId OR w.WorkerId IS NULL) AND vw.[Role] in @roles
                    AND w.IsArchived = 0";

        private readonly SqlConnection connection;

        public GetWorkersByUserIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetWorkersByUserIdQueryResult> Handle(
            GetWorkersByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var role = Principal.GetRole();
            var userId = Principal.GetUserId();
            var ids = Principal.GetVenueIds();
            var roles = new List<int> { (int)VenueUserRole.Worker };

            if (role >= VenueUserRole.Manager)
            {
                roles.Add((int)VenueUserRole.Manager);
            }
            if (role >= VenueUserRole.Director)
            {
                roles.Add((int)VenueUserRole.Director);
            }

            var workers = (await connection.QueryAsync(Query, new
            {
                ids,
                userId,
                roles
            })).ToList();

            var groupedCustomerList = workers
                .GroupBy(t => new
                {
                    t.Id,
                    t.WorkerId,
                    t.Phone,
                    t.Name,
                    t.LastName,
                    t.Email,
                    t.PhotoUri
                }).ToList();

            var items = groupedCustomerList.Select(g => new GetWorkerByIdQueryResult
            {
                Id = g.Key.Id,
                WorkerId = g.Key.WorkerId,
                Phone = g.Key.Phone,
                Name = g.Key.Name,
                LastName = g.Key.LastName,
                Email = g.Key.Email,
                PhotoUri = g.Key.PhotoUri,
                Venues = g.Select(o => new GetVenueWorkerResult
                {
                    VenueId = o.VenueId,
                    Role = (VenueUserRole)o.Role,
                    PositionName = o.PositionName
                }).ToList()
            })
                .ToList();

            return new GetWorkersByUserIdQueryResult
            {
                Result = items
            };
        }
    }
}