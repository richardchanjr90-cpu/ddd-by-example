using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms
{
    public class ArchiveLoyaltyProgramCommand : IRequest<ICommandResult>
    {
        public Guid UserId { get; set; }

        public long Id { get; set; }
    }
}