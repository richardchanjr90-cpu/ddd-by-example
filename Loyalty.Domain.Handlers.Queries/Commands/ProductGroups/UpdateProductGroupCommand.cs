using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class UpdateProductGroupCommand : IRequest<ICommandResult>
    {
        public string Id { get; set; }

        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }
    }
}
