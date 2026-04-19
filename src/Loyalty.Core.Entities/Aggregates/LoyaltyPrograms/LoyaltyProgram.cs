using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.LoyaltyProductGroups;
using Loyalty.Core.Entities.Events.LoyaltyPrograms;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Aggregates.LoyaltyPrograms
{
    public class LoyaltyProgram : TenantEntity, IAggregateRoot
    {
        public LoyaltyProgram(
            string name,
            string description,
            DateTime startDate,
            DateTime? endDate,
            long venueId,
            Uri url)
        {
            Name = name;
            Description = description;
            StartDate = startDate.ToUniversalTime();
            EndDate = endDate?.ToUniversalTime();
            VenueId = venueId;
            Url = url;

            loyaltyProductGroups = new List<LoyaltyProductGroup>();
            AddDomainEvent(new LoyaltyProgramCreatedDomainEvent(this));
        }

        private LoyaltyProgram()
        {
        }

        [Required]
        public string Name { get; private set; }

        [MaxLength(2000)]
        public string Description { get; private set; }

        [Required]
        public DateTime StartDate { get; private set; }

        [Required]
        public DateTime? EndDate { get; private set; }

        private readonly List<LoyaltyProductGroup> loyaltyProductGroups;

        public virtual IReadOnlyCollection<LoyaltyProductGroup> LoyaltyProductGroups => loyaltyProductGroups;

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; private set; }

        public bool IsPublished { get; private set; }

        public bool IsArchived { get; private set; }

        public Uri Url { get; private set; }

        public override long TenantId => VenueId;

        public void AddLoyaltyGroup(LoyaltyProductGroup item)
        {
            loyaltyProductGroups.Add(item);
            AddDomainEvent(new LoyaltyGroupCreatedDomainEvent(item));
        }

        public void Update(
            string name,
            string description,
            DateTime startDate,
            DateTime? endDate,
            bool isPublished,
            Uri url)
        {
            if (IsPublished)
            {
                throw new LoyaltyValidationException("You can't change already published program.", ErrorCode.IS_PUBLISHED);
            }

            if (LoyaltyProductGroups.Count == 0)
            {
                throw new LoyaltyValidationException("You can't publish group without any LoyaltyProductGroups attached.", ErrorCode.FAILED_TO_PUBLISH);
            }

            Name = name;
            Description = description;
            StartDate = startDate.ToUniversalTime();
            EndDate = endDate?.ToUniversalTime();
            IsPublished = isPublished;
            Url = url;

            AddDomainEvent(new LoyaltyProgramUpdatedDomainEvent(this));
        }

        public void Archive()
        {
            IsArchived = true;

            foreach (var group in LoyaltyProductGroups)
            {
                group.Archive();
            }

            AddDomainEvent(new LoyaltyProgramArchivedDomainEvent(this));
        }

        public void UpdateGroup(
            long id,
            string name,
            ProductGroup group,
            string description,
            List<LoyaltyGroupRule> rules)
        {
            var loyaltyGroup = LoyaltyProductGroups.SingleOrDefault(x => x.Id == id);

            if (loyaltyGroup == null)
            {
                throw new LoyaltyValidationException("Loyalty Group not found.", ErrorCode.INCORRECT_LOYALTY_GROUP);
            }

            if (IsPublished)
            {
                throw new LoyaltyValidationException("You can't change already published program.", ErrorCode.IS_PUBLISHED);
            }

            if (LoyaltyProductGroups.Count == 0)
            {
                throw new LoyaltyValidationException("You can't publish group without any LoyaltyProductGroups attached.", ErrorCode.FAILED_TO_PUBLISH);
            }

            Name = name;
            Description = description;

            loyaltyGroup.Update(name, group.Id, description, rules);

            AddDomainEvent(new LoyaltyGroupUpdatedDomainEvent(loyaltyGroup));
        }

        public void ArchiveGroup(long id)
        {
            var loyaltyGroup = LoyaltyProductGroups.SingleOrDefault(x => x.Id == id);

            if (loyaltyGroup != null)
            {
                AddDomainEvent(new LoyaltyGroupArchivedDomainEvent(loyaltyGroup));
            }
        }
    }
}
