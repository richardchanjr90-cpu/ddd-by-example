using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Loyalty.Infrastructure.DataAccess.Context
{
    public class EmptyMediator : IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) 
            where TNotification : INotification
        {
            throw new NotImplementedException();
        }
    }
}
