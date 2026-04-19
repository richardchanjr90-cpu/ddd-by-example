using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.LoyaltyProductGroups;

namespace Loyalty.Core.Entities.Aggregates.LoyaltyPrograms
{
    public class LoyaltyProductGroup : TenantEntity
    {
        public LoyaltyProductGroup(
            string name,
            ProductGroup productGroup,
            string description,
            long programVenueId,
            List<LoyaltyGroupRule> rules)
        {
            if (productGroup == null)
            {
                throw new LoyaltyValidationException("No product group with provided id was found.", ErrorCode.INCORRECT_PRODUCT_GROUP);
            }

            if (programVenueId != productGroup.VenueId)
            {
                throw new LoyaltyValidationException("Product Group and Program belong to different venues.", ErrorCode.INCORRECT_PRODUCT_GROUP);
            }

            if (rules == null || !rules.Any())
            {
                throw new LoyaltyValidationException("No rule Specified", ErrorCode.INCORRECT_LOYALTY_GROUP);
            }

            Name = name;
            ProductGroupId = productGroup.Id;
            Description = description;
            this.rules = new List<LoyaltyGroupRule>();
            this.rules.AddRange(rules);
        }

        private LoyaltyProductGroup()
        {
        }

        [ForeignKey(nameof(LoyaltyProgram))]
        public long LoyaltyProgramId { get; private set; }

        public LoyaltyProgram LoyaltyProgram { get;  private set;  }

        public string Name { get; private set;  }

        private readonly List<LoyaltyGroupRule> rules;

        public virtual IReadOnlyCollection<LoyaltyGroupRule> Rules => rules;

        [ForeignKey(nameof(ProductGroup))]
        public long ProductGroupId { get;  private set; }

        [MaxLength(2000)]
        public string Description { get;  private set; }

        public bool IsArchived { get;  private set;  }

        public override long TenantId => LoyaltyProgram.TenantId;

        public void Update(
            string name,
            long productGroupId,
            string description,
            List<LoyaltyGroupRule> newRules)
        {
            if (newRules == null || !newRules.Any())
            {
                throw new LoyaltyValidationException("No rule Specified", ErrorCode.INCORRECT_LOYALTY_GROUP);
            }

            Name = name;
            ProductGroupId = productGroupId;
            Description = description;

            this.rules.Clear();
            this.rules.AddRange(newRules);

            AddDomainEvent(new LoyaltyGroupUpdatedDomainEvent(this));
        }

        public void AddRule(LoyaltyGroupRule rule)
        {
            rules.Add(rule);
        }

        public void Archive()
        {
            IsArchived = true;
            AddDomainEvent(new LoyaltyGroupArchivedDomainEvent(this));
        }
    }
}
