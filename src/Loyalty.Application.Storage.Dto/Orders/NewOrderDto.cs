using System;

namespace Loyalty.Application.Storage.Dto.Orders
{
    public class NewOrderDto
    {
        public long VenueId { get; set; }

        public DateTime Date { get; set; }
    }
}
