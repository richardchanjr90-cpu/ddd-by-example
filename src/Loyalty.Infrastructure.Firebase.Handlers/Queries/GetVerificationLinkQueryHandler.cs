using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Firebase.Handlers.Queries
{
    public class GetVerificationLinkQueryHandler
        : BaseFirebaseHandler, IRequestHandler<GetVerificationLinkQuery, GetVerificationLinkQueryResult>
    {
        private readonly IHttpContextAccessor accessor;

        public GetVerificationLinkQueryHandler(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<GetVerificationLinkQueryResult> Handle(GetVerificationLinkQuery request, CancellationToken cancellationToken)
        {
            var actionCodeSettings = new ActionCodeSettings
            {
                Url = "https://loyaltyprorgam-dev.azure-api.net",
            };

            //IosBundleId = "",
            //HandleCodeInApp = true,
            //AndroidPackageName = "",
            var link = String.Empty;

            try
            {
                if (!String.IsNullOrEmpty(request.NewEmail))
                {
                    link = await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(
                        request.NewEmail, actionCodeSettings, cancellationToken);
                }
            }
            catch (Exception e)
            {
                if (!String.IsNullOrEmpty(e.Message) && e.Message.Contains("TOO_MANY_ATTEMPTS_TRY_LATER"))
                {
                    throw new LoyaltyValidationException(
                        "Failed to change email. Too many attempts.", 
                        ErrorCode.TOO_MANY_ATTEMPTS_TRY_LATER);
                }
            }

            return new GetVerificationLinkQueryResult
            {
                Link = link,
                Email = request.NewEmail
            };
        }
    }
}