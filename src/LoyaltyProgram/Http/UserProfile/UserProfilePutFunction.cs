using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.UserProfile
{
    public class UserProfilePutFunction
    {
        private readonly WorkerAppService service;

        public UserProfilePutFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("UserProfilePutFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "userprofiles")]
            [RequestBodyType(typeof(UserProfileViewModel), "UserProfileViewModel")] UserProfileViewModel model,
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(UserProfilePutFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                model = await req.Cast<UserProfileViewModel>();
                var result = await service.UpdateProfile(model, token.Principal.GetUserId());

                return new OkObjectResult(result);
            });
        }
    }
}