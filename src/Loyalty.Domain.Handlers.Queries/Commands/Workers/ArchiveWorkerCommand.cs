using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class ArchiveWorkerCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public long Id { get; set; }
    }
}