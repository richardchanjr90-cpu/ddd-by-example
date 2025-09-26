using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class ArchiveProductGroupCommand : IRequest<ICommandResult>
    {
        public Guid UserId { get; set; }

        public long Id { get; set; }
    }
}