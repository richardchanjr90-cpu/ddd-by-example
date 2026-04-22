using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Write.VenueLogo
{
    public class VenueDeleteLogoFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueDeleteLogoFunction(LoyaltyVenueImageAppService imageService, LoyaltyVenueAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.imageService = imageService;
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenueDeleteLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-logo-{id}", FileAccess.ReadWrite)]
            CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenueDeleteLogoFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var uris = await imageService.GetImages(container, "logo");

                foreach (var uri in uris)
                {
                    var blockblob = container.GetBlockBlobReference(new CloudBlockBlob(new Uri(uri)).Name);
                    var p = await blockblob.DeleteIfExistsAsync();
                }

                await service.PatchLogo(id, null, null);

                return new NoContentResult();
            });
        }
    }
}