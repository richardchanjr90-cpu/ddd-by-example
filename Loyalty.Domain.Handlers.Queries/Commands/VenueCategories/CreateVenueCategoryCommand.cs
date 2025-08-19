using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Core.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.Commands.VenueCategories
{
    public class CreateVenueCategoryCommand
    {
        public int Id { get; set; }

        public VenueCategoryType CategoryType { get; set; }
    }
}
