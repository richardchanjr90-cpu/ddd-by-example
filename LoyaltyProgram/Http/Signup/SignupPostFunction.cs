using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using FirebaseAdmin.Auth;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Signup
{
    public class SignupPostFunction
    {
        private readonly WorkerAppService service;

        public SignupPostFunction(WorkerAppService service)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("SignupPostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "signup")]
            [RequestBodyType(typeof(SignupViewModel), "SignupViewModel")] SignupViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(SignupPostFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                var role = VenueUserRole.Owner;
                var claimsDictionary = new Dictionary<string, object>();

                var phone = token.Principal.Claims.First(x => x.Type == ClaimTypes.MobilePhone).Value;
                var identity = token.Principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                foreach (var claim in token.Principal.Claims)
                {
                    claimsDictionary[claim.Type] = claim.Value;
                }

                var additionalClaims = new Dictionary<string, object>
                {
                    { ClaimTypes.Role, role.ToString() },
                    { ClaimTypes.Name, model.Name },
                    { ClaimTypes.Surname, model.Surname },
                    { ClaimTypes.Email, model.Email },
                    { ClaimTypes.StateOrProvince, model.City }
                };

                foreach (var claim in additionalClaims)
                {
                    claimsDictionary[claim.Key] = claim.Value.ToString();
                }

                var worker = await service.GetByPhone(phone);

                if (worker != null)
                {
                    var workerModel = new WorkerViewModel();
                    workerModel.WorkerId = identity;
          
                    workerModel.Email = model.Email;
                    workerModel.Name = model.Name;
                    workerModel.LastName = model.Surname;
                    workerModel.Id = worker.Id;
                    workerModel.PositionName = worker.PositionName;
                    workerModel.Phone = worker.Phone;

                    //todo: uri at startup
                    //workerModel.Role = (int) worker.Role;
                    //workerModel.VenueIds = worker.VenueIds;
                    //workerModel.PhotoUri = worker.Phone;
                    foreach (var id in worker.VenueIds)
                    {
                        token.Principal.AddVenues(id);
                    }

                    await service.CompleteSignup(workerModel);

                    role = worker.Role;
                    claimsDictionary[ClaimTypes.Role] = role.ToString();
                    claimsDictionary[ClaimConstants.VENUE_CLAIM] =
                        worker.VenueIds.Select(x => x.ToString()).ToCommaSeparatedStringOrNull();
                }

                claimsDictionary.Remove("firebase");
                claimsDictionary.Remove("auth_time");

                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(identity, claimsDictionary);

                return new NoContentResult();
            });
        }
    }
}