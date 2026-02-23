using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;

namespace Loyalty.Domain.Handlers.Firebase.Queries.Queries
{
    public class GetClientInfoFirebaseQuery : IRequest<GetClientInfoFirebaseQueryResult>
    {
        public string UserId { get; set; }

        public string GoogleAuthKey { get; set; }

        public string JsonInBase64 { get; set; }
    }
}
