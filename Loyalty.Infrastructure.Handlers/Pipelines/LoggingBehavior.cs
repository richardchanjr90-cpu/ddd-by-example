using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        var log = logFactory.CreateLogger(typeof(TRequest).Name);
        //var serializedRequest = JsonConvert.SerializeObject(request);
        //log.LogInformation($"Handling {typeof(TResponse).Name}: {serializedRequest}", serializedRequest);
        var response = await next();
        log.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }
}