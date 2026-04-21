using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class CreateWorkerCommand : IRequest<ICommandResult>
    {
        public string Phone { get; set; }
    }
}