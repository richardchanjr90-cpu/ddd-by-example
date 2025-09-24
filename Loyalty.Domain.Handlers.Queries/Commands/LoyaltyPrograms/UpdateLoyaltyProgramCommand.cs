using System;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms
{
    public class UpdateLoyaltyProgramCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime EndedDate { get; set; }

        public bool IsArchived { get; set; }

        public int VenueId { get; set; }
    }
}
