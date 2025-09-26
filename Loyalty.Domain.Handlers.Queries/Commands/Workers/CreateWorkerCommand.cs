using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class CreateWorkerCommand : IRequest<ICommandResult>
    {
        public long VenueId { get; set; }

        public string WorkerId { get; set; }

        public int Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }
    }
}
