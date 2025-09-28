using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class CreateProductCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public long ProductGroupId { get; set; }
    }
}
