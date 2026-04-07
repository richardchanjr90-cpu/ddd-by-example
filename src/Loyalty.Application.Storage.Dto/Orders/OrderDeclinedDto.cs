using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.Storage.Dto.Orders
{
    public class OrderDeclinedDto
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string UserId { get; set; }

        public string Reason { get; set; }

        public OrderStatus UpdatedStatus { get; set; }
    }
}
