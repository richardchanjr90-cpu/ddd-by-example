using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Workers
{
    public interface IPatchWorkerPhotoCommandHandler : IRequestHandler<PatchWorkerPhotoCommand, ICommandResult>
    {
    }
}