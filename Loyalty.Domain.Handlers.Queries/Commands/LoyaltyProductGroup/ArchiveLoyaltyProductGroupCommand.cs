using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup
{
    public class ArchiveLoyaltyProductGroupCommand : IRequest<ICommandResult>
    {
        public Guid UserId { get; set; }

        public long Id { get; set; }
    }
}