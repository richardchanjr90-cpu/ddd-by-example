using System.Threading.Tasks;
using Loyalty.Core.Shared;
using Loyalty.Core.Shared.Settings;
using Loyalty.Core.ViewModels;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Domain.Handlers;
using Loyalty.Venue.Service;
using LoyaltyProgram.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.Venue
{
    public static class VenuePutFunction
    {
        [FunctionName("VenuePutFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "venue")]VenueViewModel model,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"{nameof(VenuePutFunction)} was triggered.");

            var di = new HostConfigurator();
            var builder = di.BuildHost(context, log);

            var host = builder.ConfigureServices((hostContext, services) =>
            {
                services.AddMediatR(typeof(BaseHandler).Assembly);
                services.AddScoped<LoyaltyVenueAppService>();
                services.AddScoped<IMongoDataClient, MongoDataClient>();
                services.Configure<DbSettings>(hostContext.Configuration);
            })
                .ConfigureData()
                .Build();

            host.Start();

            var app = (LoyaltyVenueAppService)host.Services.GetService(typeof(LoyaltyVenueAppService));
            return new OkObjectResult(app.Update(model));
        }
    }
}
