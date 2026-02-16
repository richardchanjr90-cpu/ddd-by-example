using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using FluentValidation;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Application.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;

namespace LoyaltyProgram.Http.UserProfile
{
    public class UserProfilePatchLogoFunction
    {
        private readonly WorkerAppService service;
        private readonly LoyaltyVenueImageAppService imageService;
        private readonly IOptions<ImageSettings> settings;

        public UserProfilePatchLogoFunction(
            WorkerAppService service,
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
        [FunctionName("UserProfilePatchLogoFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "userprofiles/avatar")]
            HttpRequestMessage req,
            ILogger log,
            [FunctionToken] FunctionTokenResult token)
        {
            log.LogInformation($"{nameof(UserProfilePatchLogoFunction)} was triggered.");
            return await HandlerWrapper.WrapAsync(log, token, async () =>
               {
                   var claimsId = token.Principal.GetUserId();

                   var image = await imageService.GetImageOrNullAsync(req);

                   new VenuePhotoValidator(settings.Value)
                       .ValidateAndThrow(image);

                   using (var stream = new MemoryStream())
                   {
                       var uri = claimsId.ToLower();
                       var connectionString = Environment.GetEnvironmentVariable("QueueConnectionString");
                       var storageAccount = CloudStorageAccount.Parse(connectionString);
                       var client = storageAccount.CreateCloudBlobClient();
                       var container = client.GetContainerReference($"worker-photo-{uri}");
                       var blob = await imageService.GetBlobForImageAsync(container, $"photo-{Guid.NewGuid()}.jpg");

                       imageService.SaveImageOfWidthToStream(
                           stream,
                           image);

                       await blob.UploadFromStreamAsync(stream);

                       await service.PatchPhoto(blob.Uri.ToString(), claimsId);
                   }

                   return new OkObjectResult(new CommandResult
                   {
                       Success = true
                   });
               });
        }
    }
}
