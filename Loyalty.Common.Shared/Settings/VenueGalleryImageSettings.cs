namespace Loyalty.Common.Shared.Settings
{
    public class VenueGalleryImageSettings
    {
        public int MaxImageSizeInBytes = 1048576;
        public int MaxGalleryImageHeight { get; set; } = 1440;

        public int MaxGalleryImageWidth { get; set; } = 2560;

        public int MinGalleryImageHeight { get; set; } = 600;

        public int MinGalleryImageWidth { get; set; } = 800;

        public int MdImageWidth { get; set; } = 800;

        public int SmImageWidth { get; set; } = 400;
    }
}
