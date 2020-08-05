using System;
using System.Collections.Generic;

namespace LetsPaint.DataAccess.Models
{
    public partial class MstGallery
    {
        public int GalleryId { get; set; }
        public int GalleryTypeId { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public string Badge { get; set; }
        public int ArtistId { get; set; }
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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Modifiedby { get; set; }

        public virtual MstUsers Artist { get; set; }
        public virtual MstUsers CreatedByNavigation { get; set; }
        public virtual MstGalleryType GalleryType { get; set; }
    }
}
