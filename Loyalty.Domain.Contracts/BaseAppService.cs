using System;
using MediatR;

namespace Loyalty.Domain.Contracts
{
    public abstract class BaseAppService
    {
        public IMediator Mediator { get; }

        protected BaseAppService(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
