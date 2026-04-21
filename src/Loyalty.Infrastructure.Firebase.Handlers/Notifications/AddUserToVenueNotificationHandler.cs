using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Shared.Contracts.Constants;
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

            claims[CustomClaimsConstants.Roles] = JsonSerializer.Serialize(notification.VenueRoles);

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(notification.UserId, claims, cancellationToken);
        }
    }
}