using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Workers
{
    public class UpdateWorkerCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string WorkerId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }
    }
}