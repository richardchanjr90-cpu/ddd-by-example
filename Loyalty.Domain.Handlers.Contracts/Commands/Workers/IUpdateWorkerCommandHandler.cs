using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Workers
{
    public interface IUpdateWorkerCommandHandler : IRequestHandler<UpdateWorkerCommand, ICommandResult>
    {
    }
}
