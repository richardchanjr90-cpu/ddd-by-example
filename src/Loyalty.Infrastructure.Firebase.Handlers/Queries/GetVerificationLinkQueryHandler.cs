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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Loyalty.Infrastructure.Firebase.Handlers.Queries
{
    public class GetVerificationLinkQueryHandler
        : BaseFirebaseHandler, IRequestHandler<GetVerificationLinkQuery, GetVerificationLinkQueryResult>
    {
        private readonly IOptions<EmailSettings> options;
        private readonly ILogger<GetVerificationLinkQueryHandler> logger;

        public GetVerificationLinkQueryHandler(IOptions<EmailSettings> options, ILogger<GetVerificationLinkQueryHandler> logger)
        {
            this.options = options;
            this.logger = logger;
        }

        public async Task<GetVerificationLinkQueryResult> Handle(GetVerificationLinkQuery request, CancellationToken cancellationToken)
        {
            var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(
                request.UserId, 
                cancellationToken);

            var domain = options.Value.LinkDomain;
            var deepLink = HttpUtility.UrlEncode($"{options.Value.EmailLink}?customToken={customToken}");
            var packageName = options.Value.AndroidPackageName;
            var bundleId = options.Value.IOsBundleId;

            var customUrl =
                $"https://{domain}/?link={deepLink}&apn={packageName}$ibi={bundleId}";

            logger.LogInformation($"Custom url for the user ({request.UserId}): {request.Name} {request.Surname} " +
                                  $"is {customUrl}");

            var actionCodeSettings = new ActionCodeSettings
            {
                HandleCodeInApp = true,
                Url = customUrl
            };

            var link = String.Empty;

            try
            {
                if (!String.IsNullOrEmpty(request.NewEmail) && !request.IsEmailVerified)
                {
                    link = await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(
                        request.NewEmail, actionCodeSettings, cancellationToken);
                }
            }
            catch (FirebaseAuthException ex)
            {
                logger.LogCritical("Firebase error {@Exception}", ex);
                throw new LoyaltyValidationException("Failed to change email.", ex);
            }
            catch (Exception e)
            {
                if (!String.IsNullOrEmpty(e.Message) && e.Message.Contains("TOO_MANY_ATTEMPTS_TRY_LATER"))
                {
                    throw new LoyaltyValidationException(
                        "Failed to change email. Too many attempts.", 
                        ErrorCode.TOO_MANY_ATTEMPTS_TRY_LATER);
                }
                throw;
            }

            return new GetVerificationLinkQueryResult
            {
                Link = link,
                Email = request.NewEmail
            };
        }
    }
}