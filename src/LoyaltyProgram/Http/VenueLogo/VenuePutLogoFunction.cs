using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Http.VenueImages;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.VenueLogo
{
    public class VenuePutLogoFunction
    {
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;
        private readonly IOptions<ImageSettings> imageSettings;

        public VenuePutLogoFunction(
            LoyaltyVenueAppService service, 
            LoyaltyVenueImageAppService imageService,
            IOptions<ImageSettings> imageSettings)
        {
            this.service = service;
            this.imageService = imageService;
            this.imageSettings = imageSettings;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenuePutLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-logo-{id}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);
                var newGuid = Guid.NewGuid();
                var image = await imageService.GetImageOrNullAsync(req);

                imageService.ValidateLogo(new VenueNewBlobImageDto
                {
                    Image = image,
                    VenueId = id,
                    Index = newGuid
                });

                if (image != null)
                {
                    using (var stream = new MemoryStream())
                    using (var smStream = new MemoryStream())
                    {
                        var blob = await imageService.GetBlobForImageAsync(container, $"logo-{newGuid}.jpg");
                        var smBlob = await imageService.GetBlobForImageAsync(container, $"logo-{newGuid}-sm.jpg");

                        imageService.SaveImageOfWidthToStream(
                            stream,
                            image);
                        imageService.SaveImageOfWidthToStream(
                            smStream,
                            image,
                            imageSettings.Value.SmLogoWidth);

                        await blob.UploadFromStreamAsync(stream);
                        await smBlob.UploadFromStreamAsync(smStream);

                        await service.PatchLogo(
                            id, 
                            blob.Uri.ToString(), 
                            smBlob.Uri.ToString());
                    }
                }

                return new NoContentResult();
            });
        }
    }
}