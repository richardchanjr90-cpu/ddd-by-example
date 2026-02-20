using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class ArchiveProductGroupCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public long Id { get; set; }
    }
}