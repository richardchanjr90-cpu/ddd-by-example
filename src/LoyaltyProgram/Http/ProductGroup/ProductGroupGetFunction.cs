using System;
using System.Net;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Loyalty.Application.Venue;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;
using Loyalty.Application.ViewModels.ProductGroup;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.DataAccess.Context.Scoped;
using Loyalty.Infrastructure.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace LoyaltyProgram.Http.ProductGroup
{
    public class ProductGroupGetFunction : DisposeContextFilter<ILoyaltyTenantDbContext>
    {
        private readonly ProductGroupAppService service;

        public ProductGroupGetFunction(ProductGroupAppService service, ILoyaltyTenantDbContext context) 
            : base(context)
        {
            this.service = service;
        }

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductGroupViewModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Exception))]
        [RequestHttpHeader("Authorization", true)]
        [FunctionName("ProductGroupGetFunction")]
        public async Task<IActionResult> Run(
            long id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "productgroups/{id}")]
            HttpRequest req,
            [FunctionToken] FunctionTokenResult token,
            ILogger log)
        {
            log.LogInformation($"{nameof(ProductGroupGetFunction)} was triggered.");

            return await HandlerWrapper.WrapAsync(log, token, async () =>
            {
                return new OkObjectResult(await service.Get(id));
            });
        }
    }
}