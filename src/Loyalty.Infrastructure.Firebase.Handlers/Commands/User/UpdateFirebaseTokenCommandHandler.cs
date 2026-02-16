using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using LoyaltyUser.Domain.Handlers.Queries.Commands.User;
using MediatR;

namespace Loyalty.Infrastructure.Firebase.Handlers.Commands.User
{
    public class UpdateFirebaseTokenCommandHandler
        : BaseFirebaseHandler, IRequestHandler<UpdateFirebaseTokenCommand, ICommandResult>
    {
        public async Task<ICommandResult> Handle(UpdateFirebaseTokenCommand request, CancellationToken cancellationToken)
        {
            var claimsDictionary = new Dictionary<string, object>();

            foreach (var claim in request.Token.Principal.Claims)
            {
                claimsDictionary[claim.Type] = claim.Value;
            }

            var additionalClaims = new Dictionary<string, object>
            {
                { ClaimTypes.Name, request.Name },
                { ClaimTypes.Surname, request.Surname },
                { ClaimTypes.Email, request.Email },
            };

            foreach (var claim in additionalClaims)
            {
                claimsDictionary[claim.Key] = claim.Value.ToString();
            }

            claimsDictionary.Remove("firebase");
            claimsDictionary.Remove("auth_time");
            var identity = request.Token.Principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(identity, claimsDictionary);

            return new CommandResult()
            {
                Success = true
            };
        }
    }
}