using MediatR;

namespace Loyalty.Domain.Contracts
{
    public abstract class BaseAppService
    {
        protected BaseAppService(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }
    }
}
