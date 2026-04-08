using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.IoC;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Http.UserProfile
{
    public class UserProfilePatchEmailFunction
    {
        private readonly WorkerAppService service;
        private readonly IOptions<EmailSettings> settings;

        public UserProfilePatchEmailFunction(
            WorkerAppService service,
            IOptions<EmailSettings> settings)
        {
            this.service = service;
            this.settings = settings;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("UserProfilePatchEmailFunction")]
        public async Task<IActionResult> Run(
            string email,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "userprofiles/emails/{email}")]
            HttpRequestMessage req,
            ILogger log,
            [Queue("invite-mail", Connection = "QueueConnectionString")] ICollector<EmailInvitationDto> queueItems,
            [FunctionToken] FunctionTokenResult token)
        {
            log.LogInformation($"{nameof(UserProfilePatchEmailFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var result = await service.SetupEmail(
                    new PatchEmailViewModel()
                    {
                        Email = email
                    },
                    token.Principal.GetUserId());

                if (result != null && !String.IsNullOrEmpty(result.Link))
                {
                    queueItems.Add(new EmailInvitationDto
                    {
                        Link = result.Link,
                        CustomerEmail = result.Email,
                        SenderEmail = settings.Value.InviteEmail
                    });
                }

                return new OkObjectResult(result);
            });
        }
    }
}
