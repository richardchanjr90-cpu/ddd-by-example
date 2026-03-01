using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class CreateProductCommand : IRequest<ICommandResult>
    {
        public string Name { get; set; }

        public ProductIconType? Icon { get; set; }

        public long ProductGroupId { get; set; }
    }
}