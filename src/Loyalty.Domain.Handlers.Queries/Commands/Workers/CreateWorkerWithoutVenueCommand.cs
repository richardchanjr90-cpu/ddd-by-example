using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class CreateWorkerWithoutVenueCommand : IRequest<ICommandResult>
    {
        public string WorkerId { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }
    }
}