using System;

namespace Loyalty.Application.Storage.Dto
{
    public class VenueImage
    {
        public Byte[] Image { get; set; }

        public long VenueId { get; set; }

        public int Index { get; set; }
    }
}
