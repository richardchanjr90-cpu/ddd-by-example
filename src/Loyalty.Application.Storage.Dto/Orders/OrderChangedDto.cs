using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.Storage.Dto.Orders
{
    public class OrderChangedDto
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public OrderStatus ChangedStatus { get; set; }

        public string VenueComment { get; set; }
    }
}
