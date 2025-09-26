using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class UpdateProductCommand : IRequest<ICommandResult>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }
    }
}
