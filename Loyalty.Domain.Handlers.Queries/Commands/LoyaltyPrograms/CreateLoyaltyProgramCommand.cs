using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms
{
    public class CreateLoyaltyProgramCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime? EndedDate { get; set; }

        public long VenueId { get; set; }

        public string UserId { get; set; }
    }
}