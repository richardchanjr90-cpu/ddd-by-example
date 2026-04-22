using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Http.Write.UserProfile
{
    public class UserProfilePatchCompleteEmailFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly WorkerAppService service;
        private readonly IOptions<EmailSettings> settings;

        public UserProfilePatchCompleteEmailFunction(
            WorkerAppService service,
            IOptions<EmailSettings> settings, 
            ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
            this.settings = settings;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("UserProfilePatchCompleteEmailFunction")]
        public async Task<IActionResult> Run(
            string email,
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "userprofiles/emails/complete/{email}")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken] FunctionTokenResult token)
        {
            log.LogInformation($"{nameof(UserProfilePatchCompleteEmailFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var result = await service.CompleteEmail(
                    new PatchEmailViewModel()
                    {
                        Email = email
                    },
                    token.Principal.GetUserId());

                return new OkObjectResult(result);
            });
        }
    }
}
