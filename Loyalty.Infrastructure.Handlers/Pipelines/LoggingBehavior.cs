using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

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
        log.LogInformation($"Handling {request}");
        var response = await next();
        log.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }
}