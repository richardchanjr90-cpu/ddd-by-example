using System.Collections.Generic;
using System.Linq;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Workers
{
    public class Worker : AuditableEntity, IAggregateRoot
    {
        public Worker(
            string workerId,
            string phone,
            string name,
            string lastName)
        {
            WorkerId = workerId;
            Phone = phone;
            Name = name;
            LastName = lastName;

            venueRoles = new List<VenueWorker>();
        }

        public Worker(
            string phone,
            string name)
        {
            Phone = phone;
            Name = name;

            venueRoles = new List<VenueWorker>();
        }

        private Worker()
        {
            //ef core
        }

        public string WorkerId { get; private set; }

        public string Phone { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PhotoUri { get; private set; }

        public bool IsArchived { get; private set; }

        public virtual IReadOnlyCollection<VenueWorker> VenueRoles => venueRoles;

        private readonly List<VenueWorker> venueRoles;

        public Worker Invite(
                        long venueId,
                        VenueUserRole role,
                        string positionName,
                        VenueUserRole inviterRole)
        {
            if (role >= inviterRole)
            {
                throw new LoyaltyValidationException(
                    "Impossible to invite a user with the role that is >= current user's.",
                    ErrorCode.IMPOSSIBLE_TO_CREATE_WITH_ROLE);
            }

            if (VenueRoles.Any(x => x.VenueId == venueId))
            {
                throw new LoyaltyValidationException("Already invited to this venue", ErrorCode.DUPLICATED_ENTITY);
            }

            venueRoles.Add(new VenueWorker(venueId, Id, role, positionName));

            return this;
        }

        public Worker UpdateInvite(
            string name,
            VenueUserRole role,
            string positionName,
            long venueId,
            VenueUserRole inviterRole,
            string inviterId)
        {
            if (WorkerId != null && WorkerId.Equals(inviterId))
            {
                throw new LoyaltyValidationException(
                    "Impossible to change yourself.", 
                    ErrorCode.INVALID_ROLE);
            }

            if (role != VenueUserRole.Owner && role >= inviterRole)
            {
                throw new LoyaltyValidationException(
                    "Impossible to set a role that is higher or equals to personal.", 
                    ErrorCode.INVALID_ROLE);
            }

            if (role == VenueUserRole.Owner)
            {
                throw new LoyaltyValidationException(
                    "Impossible to create a second owner.", 
                    ErrorCode.SECOND_OWNER_NOT_ALLOWED);
            }

            var venueRole = VenueRoles
                .FirstOrDefault(x => x.VenueId == venueId);

            if (venueRole != null)
            {
                if (venueRole.Role == VenueUserRole.Owner)
                {
                    throw new LoyaltyValidationException(
                        "Impossible to change owner's role.", 
                        ErrorCode.OWNER_CHANGE_DENIED);
                }

                if (venueRole.Role > inviterRole)
                {
                    throw new LoyaltyValidationException(
                        "Impossible to change user with a higher role.", 
                        ErrorCode.INVALID_ROLE);
                }

                venueRole.UpdateRole(role, positionName);
            }

            return this;
        }

        public void Update(
            string workerId,
            string name, 
            string lastName, 
            long venueId, 
            string positionName, 
            VenueUserRole role)
        {
            if (role == VenueUserRole.Owner)
            {
                throw new LoyaltyValidationException(
                    "Impossible to create a second owner.", 
                    ErrorCode.SECOND_OWNER_NOT_ALLOWED);
            }

            WorkerId = workerId;
            Name = name;
            LastName = lastName;

            AddDomainEvent(new WorkerUpdatedDomainEvent(this, role, venueId, positionName));
        }

        public void AddToVenue(
            VenueWorker role)
        {
            venueRoles.Add(role);
        }

        public void UpdateProfile(
            string name, 
            string lastName)
        {
            Name = name;
            LastName = lastName;

            AddDomainEvent(new WorkerProfileDomainEvent(this));
        }

        public void ChangeProfilePhoto(string photoUri)
        {
            PhotoUri = photoUri;

            AddDomainEvent(new WorkerPatchDomainEvent(this));
        }

        public void CompleteInviteWithExternalId(string workerId)
        {
            WorkerId = workerId;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void Archive()
        {
            IsArchived = true;
        }
    }
}
