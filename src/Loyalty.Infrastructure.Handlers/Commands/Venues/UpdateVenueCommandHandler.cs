using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Aggregates.Venues.ValueObjects;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class UpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;
        private readonly IMediator mediator;

        public UpdateVenueCommandHandler(
            IVenueRepository venueRepository,
            IMediator mediator)
        {
            this.venueRepository = venueRepository;
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await venueRepository.GetAsync(request.Id, cancellationToken);

            if (venue == null)
            {
                throw new LoyaltyValidationException(
                    "Venue not found.",
                    ErrorCode.VENUE_NOT_FOUND);
            }
            else
            {
                var description = new VenueDetails(
                    request.FullDescription,
                    request.Description,
                    JsonSerializer.Serialize(request.WorkingHours));

                var socialNetworks = new SocialNetworks(
                    request.SocialNetworks?.Instagram,
                    request.SocialNetworks?.Facebook,
                    request.SocialNetworks?.Vkontakte);

                var contactInfo = new ContactInfo(
                    request.Phones.ToCommaSeparatedStringOrNull(),
                    request.WebSites.ToCommaSeparatedStringOrNull(),
                    socialNetworks);

                var location = new Location(
                    request.Location?.City,
                    request.Location?.Address,
                    request.Location?.Latitude ?? 0.0f,
                    request.Location?.Longitude ?? 0.0f);

                venue.UpdateVenue(
                    request.Name, 
                    location, 
                    description, 
                    contactInfo, 
                    request.CategoryType,
                    request.VenueApprovalStatus);
            }

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