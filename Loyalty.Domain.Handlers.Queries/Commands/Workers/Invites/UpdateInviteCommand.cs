using System;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class UpdateInviteCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public VenueUserRole Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string PositionName { get; set; }
    }
}