using System;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class ArchiveWorkerByUidCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public string WorkerId { get; set; }
    }
}