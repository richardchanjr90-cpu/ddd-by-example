using Loyalty.Domain.Handlers.Queries.Queries.Client;
using Loyalty.Domain.Handlers.Queries.QueryResults.Client;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Client
{
    public interface IGetClientByCodeQueryHandler : IRequestHandler<GetClientByCodeQuery, GetClientByUserCodeQueryResult>
    {
    }
}