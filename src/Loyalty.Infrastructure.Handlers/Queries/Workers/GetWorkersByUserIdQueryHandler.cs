using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersByUserIdQueryHandler : BaseDapperHandler, IGetWorkersByUserIdQueryHandler
    {
        private readonly SqlConnection connection;

        public GetWorkersByUserIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetWorkersByUserIdQueryResult> Handle(GetWorkersByUserIdQuery request,
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

            var getItems =
                @"SELECT w.*, vw.[Role] as [Role], vw.VenueId as VenueId FROM loyalty.Worker w 
                    join loyalty.VenueWorker vw on w.Id = vw.WorkerId
                    WHERE 
                    vw.VenueId in @ids AND
                    (w.WorkerId != @userId OR w.WorkerId IS NULL) AND vw.[Role] in @roles
                    AND w.IsArchived = 0";

            var workers = connection.Query(getItems, new
            {
                ids,
                userId,
                roles
            }).ToList();

            var groupedCustomerList = workers
                .GroupBy(t => new
                {
                    t.Id,
                    t.WorkerId,
                    t.Phone,
                    t.Name,
                    t.LastName,
                    t.Email,
                    t.PhotoUri,
                    t.PositionName
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
                    PositionName = g.Key.PositionName,
                    VenueIds = g.Select(o => (long)o.VenueId).ToList(),
                    Role = g.Select(o => (VenueUserRole)o.Role).FirstOrDefault()
                })
                .ToList();

            return new GetWorkersByUserIdQueryResult
            {
                Result = items
            };
        }
    }
}