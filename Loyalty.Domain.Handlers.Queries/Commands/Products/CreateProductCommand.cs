using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class CreateProductCommand : IRequest<ICommandNotificationResult>
    {
        public string Name { get; set; }

        public ProductIconType Icon { get; set; }

        public long ProductGroupId { get; set; }
    }
}