using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Pipelines
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (response is ICommandNotificationResult responseResult 
                && !responseResult.CommandResult.Success)
            {
                //Process Exceptions
            }

            return response;
        }
    }
}