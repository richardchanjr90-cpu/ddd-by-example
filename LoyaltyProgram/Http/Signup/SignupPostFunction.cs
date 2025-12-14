using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using FirebaseAdmin.Auth;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Services.AppAuthentication;
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

        [FunctionName("SignupPostFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "signup")]
            SignupViewModel model,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(SignupPostFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                var role = VenueUserRole.Owner;
                var phone = token.Principal.Claims.First(x => x.Type == ClaimTypes.MobilePhone).Value;
                var worker = await service.GetByPhone(phone);
                var identity = token.Principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var claimsDictionary = new Dictionary<string, object>();

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

                if (worker != null)
                {
                    role = (VenueUserRole)worker.Role;
                    worker.WorkerId = identity;
                    worker.Email = model.Email;

                    //todo: change to CompleteRegistration command.
                    foreach (var id in worker.VenueIds)
                    {
                        token.Principal.AddVenues(id);
                    }

                    await service.Update(worker);

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