using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;

namespace Loyalty.Infrastructure.DataAccess.Context.Scoped
{
    public abstract class DisposeContextFilter<T> :
        IFunctionExceptionFilter,
        IFunctionInvocationFilter
        where T : IDisposable
    {
        private readonly T context;

        protected DisposeContextFilter(T context)
        {
            this.context = context;
        }

        public Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            context.Dispose();
            return Task.CompletedTask;
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            context.Dispose();
            return Task.CompletedTask;
        }
    }
}
