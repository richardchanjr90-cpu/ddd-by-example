using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Shared.Contracts.Constants;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Infrastructure.Firebase.Handlers.Notifications
{
    public class SetupFirebaseTokenNotificationHandler
        : INotificationHandler<SetupFirebaseTokenNotification>
    {
        public async Task Handle(SetupFirebaseTokenNotification notification, CancellationToken cancellationToken)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(notification.WorkerId, cancellationToken);

            var claimsDictionary = user.CustomClaims.ToDictionary(
                kvp => kvp.Key, 
                kvp => kvp.Value);

            var role = VenueUserRole.Owner;

            if (notification.VenueRoles.Values.Count > 0)
            {
                role = notification.VenueRoles.Values.First();
            }

            var serialized = JsonSerializer.Serialize(notification.VenueRoles);

            var additionalClaims = new Dictionary<string, object>
            {
                { ClaimTypes.Role, role },
                { CustomClaimsConstants.Roles, serialized },
                { CustomClaimsConstants.Firstname, notification.Name },
                { CustomClaimsConstants.Lastname, notification.Surname },
                { CustomClaimsConstants.City, notification.City }
            };

            foreach (var claim in additionalClaims)
            {
                claimsDictionary[claim.Key] = claim.Value.ToString();
            }

            claimsDictionary.Remove("firebase");
            claimsDictionary.Remove("auth_time");

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(notification.WorkerId, claimsDictionary, cancellationToken);
        }
    }
}