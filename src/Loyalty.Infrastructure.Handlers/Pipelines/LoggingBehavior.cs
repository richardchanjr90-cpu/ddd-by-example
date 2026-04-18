using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Loyalty.Infrastructure.Handlers.Pipelines
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILoggerFactory logFactory;

        public LoggingBehavior(ILoggerFactory logFactory)
        {
            this.logFactory = logFactory;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var serializedRequest = JsonSerializer.Serialize(request, new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                });

                var log = logFactory.CreateLogger(typeof(TRequest)?.Name);
                log.LogInformation($"----- Handling {typeof(TResponse)?.Name}: {serializedRequest}");

                var response = await next();

                log.LogInformation($"----- Handled {typeof(TResponse)?.Name}");

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}