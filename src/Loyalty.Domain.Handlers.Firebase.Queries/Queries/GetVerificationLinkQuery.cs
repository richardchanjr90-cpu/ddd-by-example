using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;

namespace Loyalty.Domain.Handlers.Firebase.Queries.Queries
{
    public class GetVerificationLinkQuery : IRequest<GetVerificationLinkQueryResult>
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string NewEmail { get; set; }
    }
}