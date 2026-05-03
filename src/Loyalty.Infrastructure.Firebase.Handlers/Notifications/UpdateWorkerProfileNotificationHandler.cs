using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Shared.Contracts.Constants;
using MediatR;

namespace Loyalty.Infrastructure.Firebase.Handlers.Notifications
{
    public class UpdateWorkerProfileNotificationHandler
        : INotificationHandler<UpdateWorkerProfileNotification>
    {
        public async Task Handle(UpdateWorkerProfileNotification notification, CancellationToken cancellationToken)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(notification.WorkerId, cancellationToken);

            var claimsDictionary = user.CustomClaims.ToDictionary(
                kvp => kvp.Key, 
                kvp => kvp.Value);

            var additionalClaims = new Dictionary<string, object>
            {
                { CustomClaimsConstants.Firstname, notification.Name },
                { CustomClaimsConstants.Lastname, notification.LastName }
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