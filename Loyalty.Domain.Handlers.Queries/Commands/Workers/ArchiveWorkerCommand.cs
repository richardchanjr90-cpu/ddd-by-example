using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class ArchiveWorkerCommand : IRequest<ICommandResult>
    {
        public Guid UserId { get; set; }

        public long Id { get; set; }
    }
}