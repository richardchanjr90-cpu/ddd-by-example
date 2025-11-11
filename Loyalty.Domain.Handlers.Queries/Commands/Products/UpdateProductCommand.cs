using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class UpdateProductCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ProductIconType Icon { get; set; }

        public long ProductGroupId { get; set; }
    }
}