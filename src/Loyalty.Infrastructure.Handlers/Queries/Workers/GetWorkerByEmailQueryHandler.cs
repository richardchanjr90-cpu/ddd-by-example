using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkerByEmailQueryHandler
        : BaseDapperHandler, IRequestHandler<GetWorkerByEmailQuery, GetInviteByEmailQueryResult>
    {
        private readonly SqlConnection connection;

        public GetWorkerByEmailQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetInviteByEmailQueryResult> Handle(
            GetWorkerByEmailQuery request, 
            CancellationToken cancellationToken)
        {
            var email = request.Email;

            using (var transaction = new TransactionScope())
            {
                var getItems = @"SELECT TOP 1 * FROM loyalty.Worker w WHERE w.Email = @email";
                var getIds = @"SELECT * FROM loyalty.VenueWorker vw WHERE WorkerId = @id";

                var worker = connection.QuerySingleOrDefault<Worker>(getItems, new
                {
                    email
                });

                var id = worker?.Id;

                var venueWorkers = connection.Query<VenueWorker>(getIds, new
                {
                    id
                }).ToList();

                transaction.Complete();

                if (worker != null)
                {
                    worker.Venues = new List<VenueWorker>();
                    foreach (var vw in venueWorkers)
                    {
                        worker.Venues.Add(vw);
                    }

                    return worker.ToWorkerResult();
                }
            }

            return null;
        }
    }
}