using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Infrastructure.Firebase.Handlers.Notifications
{
    public class AddUserToVenueNotificationHandler
        : INotificationHandler<AddUserToVenueNotification>
    {
        public async Task Handle(AddUserToVenueNotification notification, CancellationToken cancellationToken)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(notification.UserId, cancellationToken);
            var claims = user.CustomClaims.ToDictionary(
                kvp => kvp.Key, 
                kvp => kvp.Value);

            claims[ClaimConstants.VENUE_CLAIM] = notification.VenueIds.ToCommaSeparatedStringOrNull();
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(notification.UserId, claims, cancellationToken);
        }
    }
}