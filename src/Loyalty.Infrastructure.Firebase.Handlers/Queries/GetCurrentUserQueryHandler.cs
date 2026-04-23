using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Firebase.Handlers.Queries
{
    public class GetCurrentUserQueryHandler
        : BaseFirebaseHandler, IRequestHandler<GetCurrentUserQuery, GetCurrentUserQueryResult>
    {
        private readonly IHttpContextAccessor accessor;

        public GetCurrentUserQueryHandler(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<GetCurrentUserQueryResult> Handle(
            GetCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            var userId = accessor.HttpContext.User.GetUserId();
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userId, cancellationToken);

            return new GetCurrentUserQueryResult()
            {
                Email = user.Email,
                Name = accessor.HttpContext.User.GetName(),
                UserId = userId,
                Surname = accessor.HttpContext.User.GetSurname(),
                IsEmailVerified = user.EmailVerified
            };
        }
    }
}