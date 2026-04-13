using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Aggregates.Venues.ValueObject;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class UpdateVenueCommandHandler : BaseHandler, IRequestHandler<UpdateVenueCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public UpdateVenueCommandHandler(
            ILoyaltyTenantDbContext context,
            IMediator mediator,
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (venue == null)
            {
                throw new LoyaltyValidationException(
                    "Venue not found.",
                    ErrorCode.VENUE_NOT_FOUND);
            }
            else
            {
                if (venue.VenueStatus != request.VenueApprovalStatus
                    && (request.VenueApprovalStatus == VenueApprovalStatus.Approved
                        || request.VenueApprovalStatus == VenueApprovalStatus.Rejected))
                {
                    throw new LoyaltyValidationException(
                        "Not possible to change venue's status",
                        ErrorCode.NOT_POSSIBLE_TO_APPROVE_VENUE);
                }

                if (request.VenueApprovalStatus >= VenueApprovalStatus.Published && (
                        String.IsNullOrEmpty(venue.Images) ||
                        String.IsNullOrEmpty(venue.LogoUrl)))
                {
                    throw new LoyaltyValidationException(
                        "Not possible to change venue's status",
                        ErrorCode.NOT_POSSIBLE_TO_PUBLISH_VENUE);
                }

                venue.CategoryType = request.CategoryType;
                venue.Description = request.Description;
                venue.Name = request.Name;
                venue.Type = request.Type;
                venue.FullDescription = request.FullDescription;
                venue.WebSites = request.WebSites.ToCommaSeparatedStringOrNull();
                venue.WorkingHours = JsonSerializer.Serialize(request.WorkingHours);
                venue.Phones = request.Phones.ToCommaSeparatedStringOrNull();
                venue.Address = request.Location?.Address;
                venue.City = request.Location?.City;
                venue.Latitude = request.Location?.Latitude ?? 0.0f;
                venue.Longitude = request.Location?.Longitude ?? 0.0f;
                venue.VenueStatus = request.VenueApprovalStatus;
                venue.SocialNetworks = new SocialNetworks
                {
                    Vkontakte = request.SocialNetworks?.Vkontakte,
                    Facebook = request.SocialNetworks?.Facebook,
                    Instagram = request.SocialNetworks?.Instagram,
                };
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(venue.ToUpdateNotification(), cancellationToken);
            }

            return result;
        }
    }
}