using System;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.Storage.Dto
{
    public class OrderUpdatedDto
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string UserId { get; set; }

        public OrderStatus UpdatedStatus { get; set; }
    }
}
