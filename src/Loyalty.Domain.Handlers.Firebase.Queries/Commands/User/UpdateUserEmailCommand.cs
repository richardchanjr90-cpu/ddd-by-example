using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Firebase.Queries.Commands.User
{
    public class UpdateUserEmailCommand : IRequest<ICommandResult>
    {
        public string CurrentEmail { get; set; }

        public string UserId { get; set; }

        public bool IsEmailVerified { get; set; }

        public string NewEmail { get; set; }
    }
}
