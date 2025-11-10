using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Exceptions;
using LoyaltyProgram.Http.VenueImages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace LoyaltyProgram.Http.VenueLogo
{
    public class VenuePutLogoFunction
    {
        private readonly LoyaltyVenueImageAppService service;

        public VenuePutLogoFunction(LoyaltyVenueImageAppService service)
        {
            this.service = service;
        }

        [FunctionName("VenuePutLogoFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venues/{id}/logo/")]
            HttpRequestMessage req,
            ILogger log,
            [Blob("venue-logo-{id}/logo.jpg", FileAccess.Write)]
            Stream blobStream)
        {
            log.LogInformation($"{nameof(VenuePutImageFunction)} was triggered.");

            return await ExceptionWrapper.Handle(async () =>
            {
                var image = service.GetImages(req).FirstOrDefault();

                if (image != null)
                {
                    var imageStream = Image.Load(image);
                    imageStream.SaveAsJpeg(blobStream);
                }

                return new NoContentResult();
            });
        }
    }
}