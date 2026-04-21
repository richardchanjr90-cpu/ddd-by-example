using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.UserProfile
{
    public class UpdateEmailCommand : IRequest<ICommandResult>
    {
        public string WorkerId { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }
    }
}