using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;

namespace Loyalty.Domain.Handlers.Firebase.Queries.Queries
{
    public class GetCurrentUserQuery : IRequest<GetCurrentUserQueryResult>
    {
        public string UserId { get; set; }
    }
}