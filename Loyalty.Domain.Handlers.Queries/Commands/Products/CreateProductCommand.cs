using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class CreateProductCommand : IRequest<ICommandResult>
    {
        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }
    }
}
