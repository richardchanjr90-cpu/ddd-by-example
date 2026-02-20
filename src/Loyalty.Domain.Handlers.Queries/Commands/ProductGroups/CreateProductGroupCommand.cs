using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class CreateProductGroupCommand : IRequest<ICommandResult>
    {
        public string Id { get; set; }

        public long VenueId { get; set; }

        public string Name { get; set; }

        public ProductGroupIconType Icon { get; set; }

        public List<UpdateProductCommand> Products { get; set; } = new List<UpdateProductCommand>();
    }
}