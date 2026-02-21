using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Firebase.Queries.Commands.User;
using Loyalty.Shared.Contracts.Constants;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Firebase.Handlers.Commands.User
{
    public class SetupFirebaseTokenCommandHandler
        : BaseFirebaseHandler, IRequestHandler<SetupFirebaseTokenCommand, ICommandResult>
    {
        public async Task<ICommandResult> Handle(SetupFirebaseTokenCommand request, CancellationToken cancellationToken)
        {
            var claimsDictionary = new Dictionary<string, object>();

            var identity = request.Token.Principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            foreach (var claim in request.Token.Principal.Claims)
            {
                claimsDictionary[claim.Type] = claim.Value;
            }

            var additionalClaims = new Dictionary<string, object>
            {
                { ClaimTypes.Role, request.Role },
                { CustomClaimsConstants.Firstname, Regex.Escape(request.Name) },
                { CustomClaimsConstants.Lastname, Regex.Escape(request.Surname) },
                { ClaimTypes.Email, request.Email },
                { CustomClaimsConstants.City, Regex.Escape(request.City) }
            };

            foreach (var claim in additionalClaims)
            {
                claimsDictionary[claim.Key] = claim.Value.ToString();
            }

            claimsDictionary[ClaimTypes.Role] = request.Role.ToString();

            if (!String.IsNullOrEmpty(request.VenueIds))
            {
                claimsDictionary[ClaimConstants.VENUE_CLAIM] = request.VenueIds;
            }
            else
            {
                if (request.Role != VenueUserRole.Owner)
                {
                    throw new LoyaltyValidationException("Internal Error. Should have at least 1 venue.", null, ErrorCode.INVALID_CLAIMS);
                }
            }
            
            claimsDictionary.Remove("firebase");
            claimsDictionary.Remove("auth_time");

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(identity, claimsDictionary);

            return new CommandResult()
            {
                Success = true
            };
        }
    }
}