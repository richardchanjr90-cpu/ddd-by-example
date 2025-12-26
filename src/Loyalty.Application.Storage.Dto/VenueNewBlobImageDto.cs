using System;

namespace Loyalty.Application.Storage.Dto
{
    public class VenueNewBlobImageDto
    {
        public byte[] Image { get; set; }

        public long VenueId { get; set; }

        public Guid Index { get; set; }
    }
}