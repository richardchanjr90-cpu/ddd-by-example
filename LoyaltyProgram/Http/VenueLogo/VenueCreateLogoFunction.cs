using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Http.VenueImages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueLogo
{
    public class VenueCreateLogoFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenueCreateLogoFunction(LoyaltyVenueAppService service, LoyaltyVenueImageAppService imageService)
        {
            this.service = service;
            this.imageService = imageService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenueCreateLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-logo-{id}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenueCreateImageFunction)} was triggered.");

            return await Handler.WrapAsync(token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var items = await imageService.GetCount(container);
                var image = await imageService.ConvertImage(req, id, Guid.NewGuid());

                if (items >= 1)
                {
                    return new BadRequestErrorMessageResult("Cannot create more than 1 logo");
                }

                using (var stream = new MemoryStream())
                {
                    var blob = await imageService.GetBlobForImageAsync(container, $"logo-{image.Index}.jpg");

                    imageService.SaveImageOfWidthToStream(
                        stream,
                        image.Image);

                    await blob.UploadFromStreamAsync(stream);

                    log.LogDebug($"Venue Logo created for: {id}", id);
                    await service.PatchLogo(id, blob.Uri.ToString());
                }
                return new NoContentResult();
            });
        }
    }
}
