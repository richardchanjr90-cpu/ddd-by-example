using System;
using System.Collections.Generic;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetWorkersByUserIdQueryResult
    {
        public List<GetWorkerByIdQueryResult> Result { get; set; }
            = new List<GetWorkerByIdQueryResult>();
    }
}
