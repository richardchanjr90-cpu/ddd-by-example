using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetWorkersByUserIdQueryResult
    {
        public List<GetWorkerByIdQueryResult> Result { get; set; }
            = new List<GetWorkerByIdQueryResult>();
    }
}