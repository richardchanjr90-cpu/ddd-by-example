using System;
using Loyalty.Common.Shared.Enums;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class UpdateWorkerCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public Guid WorkerId { get; set; }

        public VenueUserRole Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }

        public string PositionName { get; set; }
    }
}
