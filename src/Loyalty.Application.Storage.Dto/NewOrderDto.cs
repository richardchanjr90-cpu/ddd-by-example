using System;

namespace Loyalty.Application.Storage.Dto
{
    public class NewOrderDto
    {
        public long VenueId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
