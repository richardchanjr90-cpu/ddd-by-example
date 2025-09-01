using System.IO;

namespace Loyalty.Storage.Dto
{
    public class VenueImage
    {
        public Stream Image { get; set; }

        public long VenueId { get; set; }
    }
}
