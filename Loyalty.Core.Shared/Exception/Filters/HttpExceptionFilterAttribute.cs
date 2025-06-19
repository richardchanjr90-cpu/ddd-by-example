using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Loyalty.Core.Shared.Exception.Filters
{
    public class HttpExceptionFilterAttribute : FunctionExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            var exception = exceptionContext.Exception;

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            exceptionContext.Logger.LogError(exception.Message);
            Task t = Task.FromResult(new BadRequestObjectResult(exception.Message));
            return t;
        }
    }
}
