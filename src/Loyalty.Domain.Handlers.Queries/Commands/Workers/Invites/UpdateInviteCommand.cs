using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites
{
    public class UpdateInviteCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public VenueUserRole Role { get; set; }

        public string Name { get; set; }

        public string PositionName { get; set; }
    }
}