using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.UserProfile
{
    public class UpdateUserProfileCommand : IRequest<ICommandResult>
    {
        public string WorkerId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PositionName { get; set; }
    }
}