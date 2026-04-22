using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Aggregates.Venues.ValueObjects;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Venues
{
    public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;
        private readonly IWorkerRepository workerRepository;
        private readonly IOptions<VenueSettings> venueOptions;
        private readonly IHttpContextAccessor accessor;

        public CreateVenueCommandHandler(
            IVenueRepository venueRepository,
            IWorkerRepository workerRepository,
            IOptions<VenueSettings> venueOptions,
            IHttpContextAccessor accessor)
        {
            this.venueRepository = venueRepository;
            this.workerRepository = workerRepository;
            this.venueOptions = venueOptions;
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var userId = accessor.HttpContext.User.GetUserId();
            var worker = await workerRepository.GetByUidAsync(userId, cancellationToken);

            if (worker == null)
            {
                throw new LoyaltyValidationException("User does not exist", ErrorCode.USER_DOES_NOT_EXIST);
            }

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

            var venue = new Venue(
                request.Name,
                userId,
                location, 
                description, 
                contactInfo, 
                request.CategoryType);

            await venueRepository.AddAsync(venue);

            var result = new CommandResult
            {
                Success = await venueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = venue.Id
            };

            return result;
        }
    }
}