using Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.UserProfile
{
    public class GetUserProfileByIdQuery : IRequest<GetUserProfileByIdQueryResult>
    {
        public string UserId { get; set; }
    }
}