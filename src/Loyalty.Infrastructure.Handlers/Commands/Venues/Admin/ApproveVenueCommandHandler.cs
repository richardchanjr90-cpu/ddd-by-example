using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues.Admin
{
    public class ApproveVenueCommandHandler : IRequestHandler<ApproveVenuePatchCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;

        public ApproveVenueCommandHandler(
            IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        public async Task<ICommandResult> Handle(ApproveVenuePatchCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetWithoutQueryFiltersAsync(request.Id, cancellationToken);

            venue.Approve();
            venueRepository.Update(venue);

            var result = new CommandResult
            {
                Success = await venueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = venue.Id
            };

            return result;
        }
    }
}
