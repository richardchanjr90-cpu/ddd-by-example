using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class ArchiveProductCommand : IRequest<ICommandResult>
    {
        public string UserId { get; set; }

        public long Id { get; set; }
    }
}