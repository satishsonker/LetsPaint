using System;
using System.Collections.Generic;
using System.Text;

namespace LetsPaint.ModelAccess.Gallery
{
    public class GalleryModel
    {
        public int GalleryId { get; set; }
        public int GalleyTypeId { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public string Badge { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string SuitableFor { get; set; }
        public bool? IsAvailable { get; set; }
        public int AvailableQy { get; set; }
        public decimal Price { get; set; }
        public string Medium { get; set; }
        public string Surface { get; set; }
        public bool? HasArtistSign { get; set; }
        public bool? HasArtistCertificate { get; set; }
        public string Tags { get; set; }
    }

    public partial class GalleryTypeModel
    {
        public int GalleryTypeId { get; set; }
        public string GalleryType { get; set; }
        public string BaseUrl { get; set; }
        public int GridSize { get; set; }
    }
}
