using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using Loyalty.Shared.Contracts.Enums;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LoyaltyProgram.Http.Write.VenueImages
{
    public class VenuePutImageFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly IOptions<ImageSettings> imageSettings;
        private readonly LoyaltyVenueAppService service;
        private readonly LoyaltyVenueImageAppService imageService;

        public VenuePutImageFunction(
            IOptions<ImageSettings> imageSettings,
            LoyaltyVenueAppService service,
            LoyaltyVenueImageAppService imageService, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.imageSettings = imageSettings;
            this.service = service;
            this.imageService = imageService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ICommandResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("VenuePutImageFunction")]

        public async Task<IActionResult> Run(
            long id,
            string index,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/details/images/{index}")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken(nameof(VenueUserRole.Owner), nameof(VenueUserRole.Director))] FunctionTokenResult token,
            [Blob("venue-images-{id}/original-image-{index}.jpg", FileAccess.Write)] [SwaggerIgnore] Stream originalBlob,
            [Blob("venue-images-{id}/md-image-{index}.jpg", FileAccess.Write)] [SwaggerIgnore] Stream mediumBlob,
            [Blob("venue-images-{id}/sm-image-{index}.jpg", FileAccess.Write)] [SwaggerIgnore] Stream smallBlob)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                token.Principal.IsInRoleAndThrow(id);

                var image = await imageService.ConvertImage(req, id, Guid.Parse(index));
                imageService.ValidateImage(image);

                if (image != null)
                {
                    imageService.SaveImageOfWidthToStream(
                        originalBlob, 
                        image.Image);

                    imageService.SaveImageOfWidthToStream(
                        mediumBlob, 
                        image.Image, 
                        imageSettings.Value.MdImageWidth);

                    imageService.SaveImageOfWidthToStream(
                        smallBlob, 
                        image.Image, 
                        imageSettings.Value.SmImageWidth);
                }

                log.LogDebug($"Venue Image created for: {id}", id);
                return new NoContentResult();
            });
        }
    }
}
