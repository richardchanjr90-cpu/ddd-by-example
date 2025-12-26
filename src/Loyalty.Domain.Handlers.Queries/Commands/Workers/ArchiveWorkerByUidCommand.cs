using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class ArchiveWorkerByUidCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public string WorkerId { get; set; }
    }
}