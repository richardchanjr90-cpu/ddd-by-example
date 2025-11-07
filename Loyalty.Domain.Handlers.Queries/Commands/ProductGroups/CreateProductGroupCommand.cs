using System.Collections.Generic;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class CreateProductGroupCommand : IRequest<ICommandResult>
    {
        public string Id { get; set; }

        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public List<UpdateProductCommand> Products { get; set; } = new List<UpdateProductCommand>();
    }
}