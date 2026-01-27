using Loyalty.Domain.Handlers.Queries.Queries.Code;
using Loyalty.Domain.Handlers.Queries.QueryResults.Code;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Code
{
    public interface IGetUserInfoByCodeQueryHandler : 
        IRequestHandler<GetUserInfoByCodeQuery, GetUserInfoByCodeQueryResult>
    {
    }
}