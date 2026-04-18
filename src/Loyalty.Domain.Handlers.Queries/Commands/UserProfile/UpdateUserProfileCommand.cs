using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.UserProfile
{

    public class UpdateUserProfileCommand : IRequest<ICommandResult>
    {
        public string WorkerId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}