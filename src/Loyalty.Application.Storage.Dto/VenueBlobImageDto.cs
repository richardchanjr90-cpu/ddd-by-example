namespace Loyalty.Application.Storage.Dto
{
    public class VenueBlobImageDto
    {
        public byte[] Image { get; set; }

        public long VenueId { get; set; }

        public int Index { get; set; }
    }
}