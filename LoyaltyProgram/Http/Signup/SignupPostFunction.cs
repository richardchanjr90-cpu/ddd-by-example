using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using FirebaseAdmin.Auth;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.Signup;
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

                if (worker != null)
                {
                    role = (VenueUserRole)worker.Role;
                }

                var identity = token.Principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var additionalClaims = new Dictionary<string, object>
                {
                    { ClaimTypes.Role, role.ToString() },
                    { ClaimTypes.Name, model.Name },
                    { ClaimTypes.Surname, model.Surname },
                    { ClaimTypes.Email, model.Email }
                };

                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(identity, additionalClaims);

                if (worker != null)
                {
                    worker.WorkerId = identity;
                    await service.Update(worker);
                }

                return new NoContentResult();
            });
        }
    }
}