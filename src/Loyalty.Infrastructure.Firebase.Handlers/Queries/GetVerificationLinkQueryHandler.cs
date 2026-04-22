using System;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
            //var actionCodeSettings = new ActionCodeSettings
            //{
            //    HandleCodeInApp = true,
            //    Url = "https://zalikstage.page.link/29hQ"
            //};
            var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(request.UserId, cancellationToken);
            var domain = "zalikstage.page.link";
            var deepLink = HttpUtility.UrlEncode($"https://zalik.app?customToken={customToken}");
            var packageName = options.Value.AndroidPackageName;
            var bundleId = options.Value.IOsBundleId;

            var customUrl =
                $"https://{domain}/?link={deepLink}&apn={packageName}$ibi={bundleId}";

            var actionCodeSettings = new ActionCodeSettings
            {
                HandleCodeInApp = true,
                Url = customUrl
            };

            //if (!String.IsNullOrEmpty(options.Value.AndroidPackageName))
            //{
            //    actionCodeSettings.AndroidPackageName = options.Value.AndroidPackageName;
            //}

            //if (!String.IsNullOrEmpty(options.Value.IOsBundleId))
            //{
            //    actionCodeSettings.AndroidPackageName = options.Value.IOsBundleId;
            //}
            var link = String.Empty;

            try
            {
                if (!String.IsNullOrEmpty(request.NewEmail) && !request.IsEmailVerified)
                {
                    link = await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(
                        request.NewEmail, actionCodeSettings, cancellationToken);

                    var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(request.UserId, cancellationToken);
                    link = $"{link}" + HttpUtility.UrlEncode($"&customToken={customToken}");
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