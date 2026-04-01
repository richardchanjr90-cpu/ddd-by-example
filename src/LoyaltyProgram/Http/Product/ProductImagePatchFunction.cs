using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using FluentValidation;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Application.Storage.Dto.Validators.Interface;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyProgram.Http.UserProfile;
using LoyaltyProgram.Http.VenueImages;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LoyaltyProgram.Http.Product
{
    public class ProductImagePatchFunction
    {
        private readonly ProductAppService service;
        private readonly LoyaltyVenueImageAppService imageService;
        private readonly IOptions<ImageSettings> settings;

        public ProductImagePatchFunction(
            ProductAppService service,
            LoyaltyVenueImageAppService imageService,
            IOptions<ImageSettings> settings)
        {
            this.service = service;
            this.imageService = imageService;
            this.settings = settings;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("ProductImagePatchFunction")]
        public async Task<IActionResult> Run(
            long venueId,
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "venues/{venueId}/products/{id}/images")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director), nameof(VenueUserRole.Manager))] FunctionTokenResult token,
            [Blob("venue-product-photo-{venueId}", FileAccess.Write)] CloudBlobContainer container)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                token.Principal.IsInRoleAndThrow(venueId);
                var newGuid = Guid.NewGuid();
                var image = await imageService.GetImageOrNullAsync(req);

                imageService.ValidateProductPhoto(image);

                if (image != null)
                {
                    using (var stream = new MemoryStream())
                    using (var smStream = new MemoryStream())
                    {
                        var blob = await imageService.GetBlobForImageAsync(container, $"product-photo-{newGuid}.jpg");
                        var smBlob = await imageService.GetBlobForImageAsync(container, $"product-photo-{newGuid}-sm.jpg");

                        imageService.SaveImageOfWidthToStream(
                            stream,
                            image);
                        imageService.SaveImageOfWidthToStream(
                            smStream,
                            image,
                            settings.Value.SmLogoWidth);

                        await blob.UploadFromStreamAsync(stream);
                        await smBlob.UploadFromStreamAsync(smStream);

                        await service.PatchImages(
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
