using System;
using MediatR;

namespace Loyalty.Domain.Contracts
{
    public abstract class BaseAppService
    {
        public IMediator Mediator { get; }

        public BaseAppService(IMediator mediator)
        {
            this.Mediator = mediator;
        }
    }
}
