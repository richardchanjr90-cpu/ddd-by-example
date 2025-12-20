using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger log;

    public LoggingBehavior(ILogger log)
    {
        this.log = log;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        log.LogInformation($"Handling {typeof(TRequest).Name}");
        log.LogInformation($"Handling {request}");
        var response = await next();
        log.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }
}