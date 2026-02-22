using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkerByPhoneQueryHandler : BaseDapperHandler, IGetWorkerByPhoneQueryHandler
    {
        private readonly SqlConnection connection;

        public GetWorkerByPhoneQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetInviteByPhoneQueryResult> Handle(
                GetWorkerByPhoneQuery request,
                CancellationToken cancellationToken)
        {
            var phone = request.Phone;

            using (var transaction = new TransactionScope())
            {
                var getItems = @"SELECT TOP 1 * FROM loyalty.Worker w WHERE w.Phone = @phone";
                var getIds = @"SELECT * FROM loyalty.VenueWorker vw WHERE WorkerId = @id";

                var worker = connection.QuerySingleOrDefault<Worker>(getItems, new
                {
                    phone
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

                    return worker.ToInvite();
                }
            }

            return null;
        }
    }
}