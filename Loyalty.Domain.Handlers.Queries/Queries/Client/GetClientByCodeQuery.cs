using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Client
{
    public class GetClientByCodeQuery : IRequest<GetClientByUserCodeQueryResult>
    {
        public string UserCode { get; set; }
    }
}
