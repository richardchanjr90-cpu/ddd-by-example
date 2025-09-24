using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.VenueDetails
{
    public interface ICreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, ICommandResult>
    {
    }
}
