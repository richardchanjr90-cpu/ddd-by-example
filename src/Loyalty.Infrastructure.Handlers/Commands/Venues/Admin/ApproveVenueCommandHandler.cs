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
        private readonly IVenueAdminRepository venueAdminRepository;

        public ApproveVenueCommandHandler(
            IVenueAdminRepository venueAdminRepository)
        {
            this.venueAdminRepository = venueAdminRepository;
        }

        public async Task<ICommandResult> Handle(ApproveVenuePatchCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueAdminRepository.GetWithoutQueryFiltersAsync(request.Id, cancellationToken);

            venue.Approve();

            venueAdminRepository.Update(venue);

            var result = new CommandResult
            {
                Success = await venueAdminRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = venue.Id
            };

            return result;
        }
    }
}
