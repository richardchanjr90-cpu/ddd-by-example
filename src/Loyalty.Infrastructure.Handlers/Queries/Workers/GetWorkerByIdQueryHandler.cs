using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkerByIdQueryHandler : BaseDapperHandler, IRequestHandler<GetWorkerByIdQuery, GetWorkerByIdQueryResult>
    {
        private readonly SqlConnection connection;

        public GetWorkerByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetWorkerByIdQueryResult> Handle(
            GetWorkerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var getItemsQuery = @"SELECT 
                                       w.[Id]
                                      ,[Phone]
                                      ,[Name]
                                      ,vw.VenueId
                                      ,vw.PositionName
                                      ,vw.Role
                                      FROM loyalty.Worker w 
                                      JOIN loyalty.VenueWorker vw ON vw.WorkerId = w.Id
                                      WHERE w.Id = @id";

            var id = request.Id;

            await using (connection)
            {
                await connection.OpenAsync(cancellationToken);

                var dictionary = new Dictionary<long, GetWorkerByIdQueryResult>();

                var row = connection.Query<
                        GetWorkerByIdQueryResult,
                        GetVenueWorkerResult,
                        GetWorkerByIdQueryResult>(
                        getItemsQuery,
                        (worker, venueWorker) =>
                        {
                            if (!dictionary.TryGetValue(worker.Id, out var workerEntry))
                            {
                                workerEntry = worker;
                                workerEntry.Venues = new List<GetVenueWorkerResult>();

                                dictionary.Add(worker.Id, worker);
                            }

                            if (venueWorker != null)
                            {
                                workerEntry.Venues.Add(venueWorker);
                            }

                            return workerEntry;
                        }, new
                        {
                            id
                        },
                        splitOn: "VenueId")
                    .Distinct()
                    .SingleOrDefault();

                return row;
            }
        }
    }
}