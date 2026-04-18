using System;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Loyalty.Infrastructure.Firebase.Handlers.Queries
{
    public class GetVerificationLinkQueryHandler
        : BaseFirebaseHandler, IRequestHandler<GetVerificationLinkQuery, GetVerificationLinkQueryResult>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IOptions<EmailSettings> options;

        public GetVerificationLinkQueryHandler(IHttpContextAccessor accessor, IOptions<EmailSettings> options)
        {
            this.accessor = accessor;
            this.options = options;
        }

        public async Task<GetVerificationLinkQueryResult> Handle(GetVerificationLinkQuery request, CancellationToken cancellationToken)
        {
            var actionCodeSettings = new ActionCodeSettings
            {
                HandleCodeInApp = true,
                Url = "https://zalikstage.page.link/29hQ"
            };

            if (!String.IsNullOrEmpty(options.Value.AndroidPackageName))
            {
                actionCodeSettings.AndroidPackageName = options.Value.AndroidPackageName;
            }

            if (!String.IsNullOrEmpty(options.Value.IOsBundleId))
            {
                actionCodeSettings.AndroidPackageName = options.Value.IOsBundleId;
            }

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