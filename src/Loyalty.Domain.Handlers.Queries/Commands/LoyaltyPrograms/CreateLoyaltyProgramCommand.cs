using System;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms
{
    public class CreateLoyaltyProgramCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long VenueId { get; set; }

        public string UserId { get; set; }

        public Uri ExternalProgramUri { get; set; }
    }
}