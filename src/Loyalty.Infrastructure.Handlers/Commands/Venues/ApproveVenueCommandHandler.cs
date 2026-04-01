using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class ApproveVenueCommandHandler : BaseHandler, IRequestHandler<ApproveVenuePatchCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public ApproveVenueCommandHandler(ILoyaltyDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ApproveVenuePatchCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Where(x => x.Id == request.Id)
                .SingleAsync(cancellationToken);

            if (venue.VenueStatus == VenueApprovalStatus.Saved)
            {
                throw new LoyaltyValidationException("Venue is not Published, so it can't be approved.", ErrorCode.FAILED_APPROVE_NOT_PUBLISHED_VENUE);
            }

            var wasApproved = venue.VenueStatus == VenueApprovalStatus.Approved;
            venue.VenueStatus = VenueApprovalStatus.Approved;

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success || wasApproved)
            {
                await mediator.Publish(
                    new PatchVenueApproveNotification
                    {
                        Id = venue.Id
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}
