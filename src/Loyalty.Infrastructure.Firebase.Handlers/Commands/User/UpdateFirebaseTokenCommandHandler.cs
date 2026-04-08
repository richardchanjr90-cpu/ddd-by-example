using System.Collections.Generic;

using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Firebase.Queries.Commands.User;
using Loyalty.Shared.Contracts.Constants;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Firebase.Handlers.Commands.User
{
    public class UpdateFirebaseTokenCommandHandler
        : BaseFirebaseHandler, IRequestHandler<UpdateFirebaseTokenCommand, ICommandResult>
    {
        private readonly IHttpContextAccessor accessor;

        public UpdateFirebaseTokenCommandHandler(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(UpdateFirebaseTokenCommand request, CancellationToken cancellationToken)
        {
            var claimsDictionary = new Dictionary<string, object>();

            foreach (var claim in request.Token.Principal.Claims)
            {
                claimsDictionary[claim.Type] = claim.Value;
            }

            var additionalClaims = new Dictionary<string, object>
            {
                { CustomClaimsConstants.Firstname, Regex.Escape(request.Name) },
                { CustomClaimsConstants.Lastname, Regex.Escape(request.Surname) },
                { ClaimTypes.Email, request.Email },
            };

            foreach (var claim in additionalClaims)
            {
                claimsDictionary[claim.Key] = claim.Value.ToString();
            }

            claimsDictionary.Remove("firebase");
            claimsDictionary.Remove("auth_time");

            var identity = accessor.HttpContext.User.GetUserId();
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(identity, claimsDictionary);

            return new CommandResult()
            {
                Success = true
            };
        }
    }
}