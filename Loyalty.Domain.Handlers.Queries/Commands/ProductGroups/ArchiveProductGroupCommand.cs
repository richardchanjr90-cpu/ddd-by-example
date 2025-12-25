using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class ArchiveProductGroupCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public long Id { get; set; }
    }
}