using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class PatchProductImageCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public string ImageUri { get; set; }

        public string ImageUriSmall { get; set; }
    }
}