using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Venues
{
    public interface ICreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, ICommandResult>
    {
    }
}